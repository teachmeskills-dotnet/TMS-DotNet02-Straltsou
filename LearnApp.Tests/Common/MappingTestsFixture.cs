using AutoMapper;
using LearnApp.DAL.DTO;
using LearnApp.DAL.Mapping;

namespace LearnApp.Tests.Common
{
    public class MappingTestsFixture
    {
        /// <summary>
        /// Key/value configuration.
        /// </summary>
        public IConfigurationProvider ConfigurationProvider { get; }

        /// <summary>
        /// Object mapper.
        /// </summary>
        public IMapper Mapper { get; }

        public MappingTestsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(config =>
            {
                config.AddProfile<CardProfile>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }
    }
}
