using AutoMapper;
using LearnApp.BLL.Interfaces;
using LearnApp.DAL.DTO;
using LearnApp.DAL.Models;
using LearnApp.DAL.Persistence;
using LearnApp.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LearnApp.Tests.Controllers
{
    public class CardControllerTests
    {
        [Fact]
        public async Task RememberCard_WhenNewCardComes_ReturnOkResult()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            var repoMock = new Mock<IRepository<Card>>();

            var controller = new CardController(mapperMock.Object, repoMock.Object);

            CardDto card = new CardDto
            {
                Word = "test word",
                Definition = new string[] { "test definition1", "test definition2", "test definition3" },
                ApplicationUserId = 1
            };

            //Act
            var result = await controller.RememberCard(card);
            //var entities = repoMock.Object.GetAll();

            //Assert
            //Assert.NotEmpty(entities);
            Assert.IsType<OkResult>(result);

        }

        private DbContextOptions<ApplicationDbContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}
