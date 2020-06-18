using LearnApp.Core.Models;
using LearnApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
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
        private readonly HttpHandler handler = new HttpHandler();

        /// <summary>
        /// GET method for getting YandexTranslate model.
        /// </summary>
        /// <param name="userInput">Incoming update.</param>
        /// <returns>JSON object.</returns>
        [HttpPost]
        public async Task<ActionResult<YandexModel>> PostTranslate(string input)
        {
            return await handler.GetYandexModel(input);
        }

        /// <summary>
        /// GET method for getting Unsplash model.
        /// </summary>
        /// <param name="userInput">Incoming update.</param>
        /// <returns>JSON object.</returns>
        [HttpPost]
        public async Task<ActionResult<ImageModel>> PostPicture(string input)
        {
            return await handler.GetUnsplashModel(input);
        }

        /// <summary>
        /// GET method for getting Datamuse model.
        /// </summary>
        /// <param name="userInput">Incoming update.</param>
        /// <returns>JSON object.</returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ContextModel>>> PostContext(string input)
        {
            return await handler.GetContextModelAsync(input);
        }
    }
}
