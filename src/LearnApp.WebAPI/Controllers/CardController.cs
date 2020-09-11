using AutoMapper;
using LearnApp.BLL.Interfaces;
using LearnApp.DAL.DTO;
using LearnApp.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller which responsible for interactions with word/definition card object.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IRepository<Card> _cardRepository;
        private readonly IMapper _mapper;

        public CardController(IMapper mapper,
                              IRepository<Card> cardRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
        }

        /// <summary>
        /// Gives all cards remembered by specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("vocabulary")]
        [Authorize]
        public IActionResult GetRememberedCard(int userId)
        {
            var cards = _cardRepository.GetAll();
            var rememberedCards = cards.Where(card => card.ApplicationUserId == userId);

            return Ok(rememberedCards);
        }

        /// <summary>
        /// POST method for save information from the card to database.
        /// </summary>
        /// <param name="card">Incoming card.</param>
        /// <returns>Ok result.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RememberCard([FromBody] CardDto cardDto)
        {
            if (cardDto == null)
            {
                return BadRequest();
            }

            var modelCard = _mapper.Map<Card>(cardDto);

            _cardRepository.CreateEntity(modelCard);
            await _cardRepository.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Delete card from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var cardFromDatabase = _cardRepository.GetEntityByID(id);
            if (cardFromDatabase == null)
            {
                return NotFound();
            }

            _cardRepository.DeleteEntity(cardFromDatabase);
            await _cardRepository.SaveChangesAsync();

            return Ok();
        }
    }
}