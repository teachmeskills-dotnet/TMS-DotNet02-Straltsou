using LearnApp.BLL.Interfaces;
using LearnApp.Common.Config;
using LearnApp.Common.Helpers;
using LearnApp.DAL.Models;
using LearnApp.DAL.Persistence;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace LearnApp.BLL.Services
{
    /// <summary>
    /// Authentication and authorization manager based on JSON web token implementation.
    /// </summary>
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IRepository<ApplicationUser> _repository;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Consturctor which resolves services below.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="appSettings">Configuration of application security settings.</param>
        public JwtAuthenticationManager(ApplicationDbContext context,
                                IOptions<AppSettings> appSettings,
                                IRepository<ApplicationUser> repository,
                                IEmailService emailService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));

            if (appSettings is null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }
            _appSettings = appSettings.Value;
        }

        /// <inheritdoc/>
        public AuthenticationResponse Authenticate(AuthenticationParameters parameters, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(x => x.Login == parameters.Username);
            if (user == null || !user.IsVerified || !BC.Verify(parameters.Password, user.PasswordHash))
            {
                return null;
            }

            var jwtToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(ipAddress);

            user.RefreshTokens.Add(refreshToken);
            _context.Update(user);
            _context.SaveChanges();

            return new AuthenticationResponse(user, jwtToken, refreshToken.Token);
        }

        /// <inheritdoc/>
        public bool Register(AuthenticationParameters parameters, string origin)
        {
            var user = _context.Users.SingleOrDefault(x => x.Login == parameters.Username);
            if (user != null)
            {
                return false;
            }

            user = new ApplicationUser
            {
                Login = parameters.Username,
                Email = parameters.Username,
                PasswordHash = BC.HashPassword(parameters.Password),
                VerificationToken = RandomTokenString(),
                Created = DateTime.UtcNow
            };

            // save account
            _context.Add(user);
            _context.SaveChanges();

            // send email
            SendVerificationEmail(user, origin);

            return true;
        }

        /// <inheritdoc/>
        public AuthenticationResponse RefreshToken(string token, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                return null;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                return null;
            }

            // replace old refresh token with a new one and save
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            _context.SaveChanges();

            var jwtToken = GenerateJwtToken(user);

            return new AuthenticationResponse(user, jwtToken, newRefreshToken.Token);
        }

        /// <inheritdoc/>
        public void VerifyEmail(string token)
        {
            var account = _context.Users.SingleOrDefault(x => x.VerificationToken == token);

            if (account == null)
            {
                throw new Exception("Verification failed");
            }

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            _context.Users.Update(account);
            _context.SaveChanges();
        }

        /// <summary>
        /// Generarate and sends the email message with verification token.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="origin">Host title.</param>
        private void SendVerificationEmail(ApplicationUser user, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/api/account/verify-email?token={user.VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{user.VerificationToken}</code></p>";
            }

            _emailService.Send(
                to: user.Email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
            );
        }

        /// <summary>
        /// Generate JSON web token based on security key, establish claims.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns></returns>
        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.SecretEncryptionKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Generate refresh token from the random set of bytes.
        /// </summary>
        /// <param name="ipAddress">Current ip address from the context.</param>
        /// <returns></returns>
        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);

                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        /// <summary>
        /// Generate random verification token.
        /// </summary>
        /// <returns></returns>
        private string RandomTokenString()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[40];
                rngCryptoServiceProvider.GetBytes(randomBytes);

                return BitConverter.ToString(randomBytes).Replace("-", "");
            }
        }
    }
}
