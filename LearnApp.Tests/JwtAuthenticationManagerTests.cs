using LearnApp.BLL.Interfaces;
using LearnApp.BLL.Services;
using LearnApp.Common.Config;
using LearnApp.Common.Helpers;
using LearnApp.DAL.Models;
using LearnApp.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using Xunit;
using BC = BCrypt.Net.BCrypt;

namespace LearnApp.Tests
{
    public class JwtAuthenticationManagerTests
    {
        ApplicationDbContext _context;
        IOptions<AppSettings> _options;
        IRepository<ApplicationUser> _repository;
        IEmailService _emailService;

        public JwtAuthenticationManagerTests()
        {
            var contextOptions = GetDbContextOptions();
            _context = new ApplicationDbContext(contextOptions);
            _repository = new Repository<ApplicationUser>(_context);
            _options = Options.Create(new AppSettings
            {
                SecretEncryptionKey = "supersecret123testing456key",
                EmailFrom = "test@test.com",
                SmtpHost = "test@test.com",
                SmtpPort = 111,
                SmtpUser = "testUser",
                SmtpPass = "testPassword"
            });
            _emailService = new EmailService(_options);
        }


        [Fact]
        public void Authenticate_WhenCurrentUserExists_ReturnNotNullResponse()
        {
            //Arrange
            var user = new ApplicationUser
            {
                Id = 111,
                Login = "test@test.com",
                Verified = DateTime.Now,
                PasswordHash = BC.HashPassword("test123"),
            };
            _context.Add(user);
            _context.SaveChanges();

            var parameters = new AuthenticationParameters { Username = "test@test.com", Password = "test123" };
            var ip = "127.0.0.1";

            var manager = new JwtAuthenticationManager(_context, _options, _repository, _emailService);

            //Act
            var authResponse = manager.Authenticate(parameters, ip);

            //Assert
            Assert.Equal("test@test.com", authResponse.Username);
            Assert.Equal(111, authResponse.Id);
            Assert.NotNull(authResponse.JwtToken);
            Assert.NotNull(authResponse.RefreshToken);
        }

        [Fact]
        public void RefreshToken_WhenAuthenticatedUserRequestsForRefresh_ReturnNewRefreshedTokensResponse()
        {
            //Arrange
            var user = new ApplicationUser
            {
                Id = 111,
                Login = "test@test.com",
                Verified = DateTime.Now,
                PasswordHash = BC.HashPassword("test123"),
            };
            _context.Add(user);
            _context.SaveChanges();

            var parameters = new AuthenticationParameters { Username = "test@test.com", Password = "test123" };
            var ip = "127.0.0.1";

            var manager = new JwtAuthenticationManager(_context, _options, _repository, _emailService);

            //Act
            var authResponse = manager.Authenticate(parameters, ip);
            var refreshedAuthResponse = manager.RefreshToken(authResponse.RefreshToken, ip);

            //Assert
            //Assert.NotEqual(refreshedAuthResponse.JwtToken, authResponse.JwtToken); 
            Assert.NotEqual(refreshedAuthResponse.RefreshToken, authResponse.RefreshToken);
            Assert.NotNull(refreshedAuthResponse.JwtToken);
            Assert.NotNull(refreshedAuthResponse.RefreshToken);
        }

        private DbContextOptions<ApplicationDbContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}
