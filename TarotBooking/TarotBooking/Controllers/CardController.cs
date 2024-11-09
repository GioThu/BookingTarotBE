using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;
using AutoMapper;
using Service.Model.CardModel;
using Microsoft.AspNetCore.Authorization;
using BE.Attributes;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardWebController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;

        public CardWebController(ICardService cardService, IMapper mapper)
        {
            _cardService = cardService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("cards-list")]
        public async Task<List<CardDto>> GetAllCards()
        {
            var cards = await _cardService.GetAllCards();
            return _mapper.Map<List<CardDto>>(cards);
        }


        [HttpPost]
        [Route("create-card")]
        public async Task<CardDto?> CreateCard([FromForm] CreateCardModel createCardModel)
        {
            var card = await _cardService.CreateCard(createCardModel);
            return _mapper.Map<CardDto>(card);
        }

        [HttpPost]
        [Route("create-list-card")]
        public async Task<CardDto?> CreateListCard([FromForm] List<CreateCardModel> createListCardModel)
        {
            var card = await _cardService.CreatelistCard(createListCardModel);
            return _mapper.Map<CardDto>(card);
        }

        [HttpPost]
        [Route("update-card")]
        public async Task<CardDto?> UpdateCard([FromForm] UpdateCardModel updateCardModel)
        {
            var card = await _cardService.UpdateCard(updateCardModel);
            return _mapper.Map<CardDto>(card);
        }

        [HttpPost]
        [Route("delete-card")]
        public async Task<bool> DeleteCard(String cardId)
        {
            return await _cardService.DeleteCard(cardId);
        }


        [HttpGet("cards-by-group/{groupId}")]
        public async Task<ActionResult<List<CardDto>>> GetListCardByGroupId(string groupId)
        {
            try
            {
                var cards = await _cardService.GetListCardByGroupId(groupId);
                if (cards == null || !cards.Any())
                {
                    return NotFound("No cards found for the specified group ID.");
                }

                return Ok(_mapper.Map<List<CardDto>>(cards));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
