using AutoMapper;
using LearnApp.Common.Interfaces;
using LearnApp.Core.Models;
using LearnApp.Core.Services;
using LearnApp.DAL.DTO;
using LearnApp.DAL.Models;
using LearnApp.DAL.Models.ImageModel;
using Microsoft.AspNetCore.Authentication;
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
        private readonly IRepository<Card> _repository;
        private readonly IMapper _mapper;
        private readonly HttpHandler _handler;

        public ContentController(IOptions<ApiConfig> options, HttpHandler handler, IMapper mapper, IRepository<Card> repository)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(handler));
        }

        /// <summary>
        /// GET method for getting YandexTranslate model.
        /// </summary>
        /// <param name="userInput">Incoming update.</param>
        /// <returns>JSON object.</returns>
        [HttpGet("translate")]
        [Authorize]
        public async Task<ActionResult<YandexModel>> Translate(string input)
        {
            var accessToken = await HttpContext.GetTokenAsync("refreshToken");


            return await _handler.GetYandexModelAsync(input);
        }

        /// <summary>
        /// GET method for getting Unsplash model.
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
        /// GET method for getting Datamuse model.
        /// </summary>
        /// <param name="userInput">Incoming update.</param>
        /// <returns>JSON object.</returns>
        [HttpGet("context")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ContextModel>>> Context(string input)
        {
            return await _handler.GetContextModelAsync(input);
        }


        /// <summary>
        /// POST method for save information from the card.
        /// </summary>
        /// <param name="card">Incoming card.</param>
        /// <returns>Ok result.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RememberCard([FromBody] CardDto cardDto)
        {
            var modelCard = _mapper.Map<Card>(cardDto);

            _repository.CreateEntity(modelCard);
            await _repository.SaveChangesAsync();

            return Ok();
        }
    }
}
