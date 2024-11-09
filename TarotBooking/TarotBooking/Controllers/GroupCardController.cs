using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.GroupCardModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;
using AutoMapper;
using Service.Model.GroupCardModel;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupCardWebController : ControllerBase
    {
        private readonly IGroupCardService _groupCardService;
        private readonly IMapper _mapper;

        public GroupCardWebController(IGroupCardService groupCardService, IMapper mapper)
        {
            _groupCardService = groupCardService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("groupCards-list")]
        public async Task<List<GroupCardDto>> GetAllGroupCards()
        {
            var groupCards = await _groupCardService.GetAllGroupCards();
            return _mapper.Map<List<GroupCardDto>>(groupCards);
        }

        [HttpPost]
        [Route("create-groupCard")]
        public async Task<GroupCardDto?> CreateGroupCard([FromForm] CreateGroupCardModel createGroupCardModel)
        {
            var groupCard = await _groupCardService.CreateGroupCard(createGroupCardModel);
            return _mapper.Map<GroupCardDto>(groupCard);
        }

        [HttpPost]
        [Route("delete-groupCard")]
        public async Task<bool> DeleteGroupCard([FromForm] GroupCard groupCard)
        {
            return await _groupCardService.DeleteGroupCard(groupCard.Id);
        }

        [HttpGet("GetGroupCardsByReaderId/{readerId}")]
        public async Task<IActionResult> GetGroupCardsByReaderId(string readerId, int pageNumber = 1, int pageSize = 10)
        {
            var groupCards = await _groupCardService.GetGroupCardsByReaderIdAsync(readerId, pageNumber, pageSize);
            var result = _mapper.Map<List<GroupCardDto>>(groupCards);
            return Ok(result);
        }

        [HttpGet("GetGroupCardsCountByReaderId/{readerId}")]
        public async Task<IActionResult> GetGroupCardsCountByReaderId(string readerId)
        {
            try
            {
                int count = await _groupCardService.GetGroupCardsCountByReaderIdAsync(readerId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetGroupCardsWithImagesByReaderId/{readerId}")]
        public async Task<IActionResult> GetGroupCardsWithImagesByReaderId(
            string readerId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var groupCardsWithImages = await _groupCardService.GetGroupCardsWithImageByReaderIdAsync(readerId, pageNumber, pageSize);
            return Ok(groupCardsWithImages);
        }
    }
}
