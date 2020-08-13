using LearnApp.BLL.Interfaces;
using LearnApp.Common.Config;
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

namespace LearnApp.BLL.Services
{
    /// <summary>
    /// Authentication manager based on JSON web token implementation.
    /// </summary>
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Consturctor which resolves services below.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="appSettings">Configuration of application security settings.</param>
        public JwtAuthenticationManager(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Generate and attaches to response the JWT and refresh token based on giving parameters and IP address.
        /// </summary>
        /// <param name="parameters">Authentication parameters - login, password.</param>
        /// <param name="ipAddress">IP address of current user.</param>
        /// <returns></returns>
        public AuthenticationResponse Authenticate(AuthenticationParameters parameters, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(x => x.Login == parameters.Username && x.Password == parameters.Password);
            if (user == null)
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

        /// <summary>
        /// Generate a new refresh token based on giving last updated refresh token.
        /// </summary>
        /// <param name="token">Refresh token.</param>
        /// <param name="ipAddress">IP address of current user.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Generate JSON web token based on security key, establish claims.
        /// </summary>
        /// <param name="user"></param>
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
        /// <param name="ipAddress"></param>
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
    }
}
