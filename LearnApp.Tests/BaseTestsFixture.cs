using AutoMapper;
using LearnApp.DAL.Persistence;
using System;

namespace LearnApp.Tests
{
    /// <summary>
    /// Base tests fixture.
    /// </summary>
    public class BaseTestsFixture : IDisposable
    {
        /// <summary>
        /// Context of sample database.
        /// </summary>
        public ApplicationDbContext Context { get; }

        /// <summary>
        /// AutoMapper for DTO and base models.
        /// </summary>
        public IMapper Mapper { get; }

        /// <summary>
        /// Define base tests fixture.
        /// </summary>
        public BaseTestsFixture()
        {
            Context = ApplicationContextFactory.Create();

            //var configurationProvider = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<CardProfile>();
            //});

            //Mapper = configurationProvider.CreateMapper();
        }

        /// <summary>
        /// Destroy the context.
        /// </summary>
        public void Dispose()
        {
            ApplicationContextFactory.Destroy(Context);
        }
    }
}
