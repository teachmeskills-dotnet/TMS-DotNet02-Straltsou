using LearnApp.WebAPI.Models;
using LearnApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LearnApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller for processing translate requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TranslateController : ControllerBase
    {
        private readonly IOptions<ApiConfig> _options;

        /// <summary>
        /// Constructor which serve as transmitter of secret data.
        /// </summary>
        /// <param name="options">Secret data, keys.</param>
        public TranslateController(IOptions<ApiConfig> options)
        {
            _options = options;
        }

        /// <summary>
        /// GET method for getting YandexTranslate model.
        /// </summary>
        /// <param name="userInput">User text input.</param>
        /// <returns>JSON object.</returns>
        [HttpGet("{userInput}")]
        public async Task<ActionResult<YandexModel>> Get(string userInput)
        {
            var handler = new HttpHandler();

            var host = "https://translate.yandex.net/";
            var segments = new List<string>
            {
                "api",
                "v1.5",
                "tr.json",
                "translate"
            };
            object[] path = segments.ToArray();
            var parameters = new { key = _options.Value.YandexAPI, text = userInput, lang = "en-ru" };

            return await handler.GetJsonResultAsync<YandexModel>(host, path, parameters);
        }
    }
}
