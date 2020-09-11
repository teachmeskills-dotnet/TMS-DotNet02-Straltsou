using LearnApp.DAL.Models;
using LearnApp.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using BC = BCrypt.Net.BCrypt;

namespace LearnApp.Tests
{
    public static class ApplicationContextFactory
    {
        /// <summary>
        /// Create database.
        /// </summary>
        /// <returns>Context of sample database.</returns>
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        /// <summary>
        /// Fill database with seed sample data.
        /// </summary>
        /// <param name="context">Context of sample database.</param>
        public static void SeedSampleData(ApplicationDbContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            var applicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = 11,
                    Login = "test@test.com",
                    Verified = DateTime.Now,
                    PasswordHash = BC.HashPassword("test123")
                },

                new ApplicationUser
                {
                    Id = 111,
                    Login = "test@test1.com",
                    Verified = null,
                    PasswordHash = BC.HashPassword("test123")
                }
            };

            context.Users.AddRange(applicationUsers);
            context.SaveChanges();
        }

        /// <summary>
        /// Destroy database.
        /// </summary>
        /// <param name="context">Context of sample database.</param>
        public static void Destroy(ApplicationDbContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
