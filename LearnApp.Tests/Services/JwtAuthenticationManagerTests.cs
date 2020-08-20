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
    public class JwtAuthenticationManagerTests : BaseTestsFixture
    {
        IJwtAuthenticationManager _jwtAuthenticationManager;
        IOptions<AppSettings> _options;
        IRepository<ApplicationUser> _repository;
        IEmailService _emailService;

        public JwtAuthenticationManagerTests()
        {
            _repository = new Repository<ApplicationUser>(Context);
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
            _jwtAuthenticationManager = new JwtAuthenticationManager(Context, _options, _repository, _emailService);
        }


        [Fact]
        public void Authenticate_WhenCurrentUserExists_ReturnNotNullResponse()
        {
            //Arrange
            var parameters = new AuthenticationParameters { Username = "test@test.com", Password = "test123" };
            var ip = "127.0.0.1";

            //Act
            var authResponse = _jwtAuthenticationManager.Authenticate(parameters, ip);

            //Assert
            Assert.Equal("test@test.com", authResponse.Username);
            Assert.Equal(11, authResponse.Id);
            Assert.NotNull(authResponse.JwtToken);
            Assert.NotNull(authResponse.RefreshToken);
        }

        [Fact]
        public void Authenticate_WhenCurrentUserExistsButNotVerified_ReturnNullResponse()
        {
            //Arrange
            var parameters = new AuthenticationParameters { Username = "test@test1.com", Password = "test123" };
            var ip = "127.0.0.1";

            //Act
            var authResponse = _jwtAuthenticationManager.Authenticate(parameters, ip);

            //Assert
            Assert.Null(authResponse);
        }

        [Fact]
        public void RefreshToken_WhenAuthenticatedUserRequestsForRefresh_ReturnNewRefreshedTokensResponse()
        {
            //Arrange
            var parameters = new AuthenticationParameters { Username = "test@test.com", Password = "test123" };
            var ip = "127.0.0.1";

            //Act
            var authResponse = _jwtAuthenticationManager.Authenticate(parameters, ip);
            var refreshedAuthResponse = _jwtAuthenticationManager.RefreshToken(authResponse.RefreshToken, ip);

            //Assert
            //Assert.NotEqual(refreshedAuthResponse.JwtToken, authResponse.JwtToken); 
            Assert.NotEqual(refreshedAuthResponse.RefreshToken, authResponse.RefreshToken);
            Assert.NotNull(refreshedAuthResponse.JwtToken);
            Assert.NotNull(refreshedAuthResponse.RefreshToken);
        }
    }
}
