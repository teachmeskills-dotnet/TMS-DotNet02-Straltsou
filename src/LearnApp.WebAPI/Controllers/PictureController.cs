using LearnApp.WebAPI.Models;
using LearnApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller for processing picture requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PictureController : ControllerBase
    {
        private readonly IOptions<ApiConfig> _options;

        /// <summary>
        /// Constructor which serve as transmitter of secret data.
        /// </summary>
        /// <param name="options">Secret data, keys.</param>
        public PictureController(IOptions<ApiConfig> options)
        {
            _options = options;
        }

        /// <summary>
        /// GET method for getting picture model.
        /// </summary>
        /// <param name="userInput">User text input.</param>
        /// <returns>JSON object.</returns>
        [HttpGet("{userInput}")]
        public async Task<ActionResult<ImageModel>> Get(string userInput)
        {
            var handler = new HttpHandler();

            var host = "https://api.unsplash.com/";
            var segments = new List<string>
            {
                "photos",
                "random"
            };
            object[] path = segments.ToArray();
            var parameters = new { client_id = _options.Value.UnsplashAPI, query = userInput };

            return await handler.GetJsonResultAsync<ImageModel>(host, path, parameters);
        }
    }
}
