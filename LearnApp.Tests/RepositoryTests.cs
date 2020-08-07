﻿using LearnApp.BLL.Services;
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
        Repository<ApplicationUser> userRepository;

        public RepositoryTests()
        {
            var contextOptions = GetDbContextOptions();
            context = new ApplicationDbContext(contextOptions);
            userRepository = new Repository<ApplicationUser>(context);
        }

        [Fact]
        public void CreateEntity_WhenCreateNewEntity_ReturnsNotNull()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser { Id = 1, Password = "test123" };

            //Act
            userRepository.CreateEntity(user);
            context.SaveChanges();

            var currentUser = userRepository.GetEntityByID(1);

            //Assert
            Assert.NotNull(currentUser);

        }

        [Fact]
        public void DeleteEntity_WhenDeleteExistingEntity_ReturnEmpty()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser { Id = 1, Password = "test123" };
            userRepository.CreateEntity(user);
            context.SaveChanges();

            //Act
            userRepository.DeleteEntity(user);
            context.SaveChanges();

            var users = userRepository.GetAll();

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