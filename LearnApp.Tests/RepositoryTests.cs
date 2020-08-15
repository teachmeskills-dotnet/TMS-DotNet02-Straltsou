using LearnApp.BLL.Services;
using LearnApp.DAL.Models;
using LearnApp.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace LearnApp.Tests
{
    public class RepositoryTests
    {
        ApplicationDbContext context;
        Repository<ApplicationUser> _userRepository;

        public RepositoryTests()
        {
            var contextOptions = GetDbContextOptions();
            context = new ApplicationDbContext(contextOptions);
            _userRepository = new Repository<ApplicationUser>(context);
        }

        [Fact]
        public void CreateEntity_WhenCreateNewEntity_ReturnsNotNull()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser { Id = 1, PasswordHash = "test123" };

            //Act
            _userRepository.CreateEntity(user);
            context.SaveChanges();

            var currentUser = _userRepository.GetEntityByID(1);

            //Assert
            Assert.NotNull(currentUser);

        }

        [Fact]
        public void DeleteEntity_WhenDeleteExistingEntity_ReturnEmpty()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser { Id = 1, PasswordHash = "test123" };
            _userRepository.CreateEntity(user);
            context.SaveChanges();

            //Act
            _userRepository.DeleteEntity(user);
            context.SaveChanges();

            var users = _userRepository.GetAll();

            //Assert
            Assert.Empty(users);
        }

        private DbContextOptions<ApplicationDbContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}
