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
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IRepository<Card> _repository;
        private readonly IMapper _mapper;

        public CardController(IMapper mapper, IRepository<Card> repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
            var cards = _repository.GetAll();
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
            if(cardDto == null)
            {
                return BadRequest();
            }

            var modelCard = _mapper.Map<Card>(cardDto);

            _repository.CreateEntity(modelCard);
            await _repository.SaveChangesAsync();

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
            var cardFromDatabase = _repository.GetEntityByID(id);
            if (cardFromDatabase == null)
            {
                return NotFound();
            }

            _repository.DeleteEntity(cardFromDatabase);
            await _repository.SaveChangesAsync();

            return Ok();
        }
    }
}