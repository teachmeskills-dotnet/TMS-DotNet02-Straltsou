using LearnApp.Core.Models;
using LearnApp.Core.Services;
using LearnApp.DAL.Models.ImageModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LearnApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller for processing translate requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IOptions<ApiConfig> _options;
        private readonly HttpHandler _handler;

        public ContentController(IOptions<ApiConfig> options, HttpHandler handler)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        /// <summary>
        /// GET method for getting YandexTranslate model.
        /// </summary>
        /// <param name="userInput">Incoming update.</param>
        /// <returns>JSON object.</returns>
        [HttpGet("translate")]
        public async Task<ActionResult<YandexModel>> Translate(string input)
        {
            return await _handler.GetYandexModel(input);
        }

        // api/content/picture?inputTwo=qweqwe
        // api/content/picture
        /// <summary>
        /// GET method for getting Unsplash model.
        /// </summary>
        /// <param name="userInput">Incoming update.</param>
        /// <returns>JSON object.</returns>
        [HttpGet("picture")]
        public async Task<ActionResult<ImageModel>> Picture(string input)
        {
            return await _handler.GetUnsplashModel(input);
        }

        /// <summary>
        /// GET method for getting Datamuse model.
        /// </summary>
        /// <param name="userInput">Incoming update.</param>
        /// <returns>JSON object.</returns>
        [HttpGet("context")]
        public async Task<ActionResult<IEnumerable<ContextModel>>> Context(string input)
        {
            return await _handler.GetContextModelAsync(input);
        }
    }
}
