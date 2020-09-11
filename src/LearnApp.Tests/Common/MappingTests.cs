using AutoMapper;
using LearnApp.DAL.DTO;
using LearnApp.DAL.Models;
using System;
using Xunit;

namespace LearnApp.Tests.Common
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)

        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Mapper_ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Card), typeof(CardDto))]
        public void Mapper_ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);
            _mapper.Map(instance, source, destination);
        }
    }
}
