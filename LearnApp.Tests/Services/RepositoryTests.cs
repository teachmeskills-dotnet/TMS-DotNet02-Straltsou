using LearnApp.BLL.Services;
using LearnApp.DAL.Models;
using Xunit;

namespace LearnApp.Tests
{
    public class RepositoryTests : BaseTestsFixture
    {
        private Repository<ApplicationUser> _userRepository;

        public RepositoryTests()
        {
            _userRepository = new Repository<ApplicationUser>(Context);
        }

        [Fact]
        public async void CreateEntity_WhenCreateNewEntity_ReturnsNotNull()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser { Id = 1111, PasswordHash = "test123" };

            //Act
            _userRepository.CreateEntity(user);
            await _userRepository.SaveChangesAsync();

            var currentUser = _userRepository.GetEntityByID(1111);

            //Assert
            Assert.NotNull(currentUser);
        }

        [Fact]
        public void DeleteEntity_WhenDeleteExistingEntity_ReturnEmpty()
        {
            //Arrange
            ApplicationUser user = _userRepository.GetEntityByID(111);

            //Act
            _userRepository.DeleteEntity(user);
            _userRepository.SaveChanges();

            var removedUser = _userRepository.GetEntityByID(11111);

            //Assert
            Assert.Null(removedUser);
        }
    }
}
