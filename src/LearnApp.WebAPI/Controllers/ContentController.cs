using LearnApp.BLL.Interfaces;
using LearnApp.Core.Models;
using LearnApp.DAL.Models.ImageModel;
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
        public async Task<ActionResult<YandexModel>> Translate(string input)
        {
            return await _handler.GetYandexModelAsync(input);
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


        ///// <summary>
        ///// POST method for save information from the card to database.
        ///// </summary>
        ///// <param name="card">Incoming card.</param>
        ///// <returns>Ok result.</returns>
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> RememberCard([FromBody] CardDto cardDto)
        //{
        //    var modelCard = _mapper.Map<Card>(cardDto);

        //    _repository.CreateEntity(modelCard);
        //    await _repository.SaveChangesAsync();

        //    return Ok();
        //}

        ///// <summary>
        ///// Delete card from the database.
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public ActionResult DeleteCard(int id)
        //{
        //    var cardFromDatabase = _repository.GetEntityByID(id);
        //    if (cardFromDatabase == null)
        //    {
        //        return NotFound();
        //    }

        //    _repository.DeleteEntity(cardFromDatabase);
        //    _repository.SaveChanges();

        //    return Ok();
        //}

        ///// <summary>
        ///// Gives all cards remembered by specific user.
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //[HttpGet("vocabulary")]
        //public IActionResult GetRememberedCard(int userId)
        //{
        //    var cards = _repository.GetAll();
        //    var rememberedCards = cards.Where(card => card.ApplicationUserId == userId);

        //    return Ok(rememberedCards);
        //}
    }
}
