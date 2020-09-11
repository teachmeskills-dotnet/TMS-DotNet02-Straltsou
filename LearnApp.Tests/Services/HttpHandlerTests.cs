using LearnApp.BLL.Services;
using LearnApp.Common.Config;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace LearnApp.Tests
{
    public class HttpHandlerTests
    {
        [Fact]
        public async void GetContextModelAsync_WhenParameterIsGiven_ReturnDeserializedJSONList()
        {
            //Arrange
            IOptions<ApiConfig> options = Options.Create(new ApiConfig());
            var handler = new HttpHandler(options);

            //Act
            var result = await handler.GetContextModelAsync("test");

            //Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetContextModelAsync_WhenParameterIsNull_ThrowNewException()
        {
            //Arrange
            IOptions<ApiConfig> options = Options.Create(new ApiConfig());
            var handler = new HttpHandler(options);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.GetContextModelAsync(null));
        }
    }
}
