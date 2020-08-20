using LearnApp.BLL.Interfaces;
using LearnApp.Common.Config;
using LearnApp.Common.Helpers.OuterAPI;
using LearnApp.Common.Helpers.OuterAPI.ImageModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LearnApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller for processing content requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IOptions<ApiConfig> _options;
        private readonly IHttpHandler _handler;

        /// <summary>
        /// Consturctor which resolves services below.
        /// </summary>
        /// <param name="options">API configuration options.</param>
        /// <param name="handler">Service which serves as a handler for user requests.</param>
        /// <param name="mapper">Auto mapper for mapping models.</param>
        /// <param name="repository">Card repository.</param>
        public ContentController(IOptions<ApiConfig> options, IHttpHandler handler)
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
        [Authorize]
        public async Task<ActionResult<TranslateModel>> Translate(string input)
        {
            return await _handler.GetTranslateModelAsync(input);
        }

        /// <summary>
        /// GET method for getting picture Unsplash model.
        /// </summary>
        /// <param name="userInput">Incoming update.</param>
        /// <returns>JSON object.</returns>
        [HttpGet("picture")]
        [Authorize]
        public async Task<ActionResult<ImageModel>> Picture(string input)
        {
            return await _handler.GetUnsplashModelAsync(input);
        }

        /// <summary>
        /// GET method for getting Datamuse context model.
        /// </summary>
        /// <param name="userInput">Incoming update.</param>
        /// <returns>JSON object.</returns>
        [HttpGet("context")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ContextModel>>> Context(string input)
        {
            return await _handler.GetContextModelAsync(input);
        }
    }
}
