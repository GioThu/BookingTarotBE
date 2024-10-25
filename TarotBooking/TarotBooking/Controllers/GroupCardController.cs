using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.GroupCardModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupCardWebController : ControllerBase
    {
        private readonly IGroupCardService _groupCardService;
        public GroupCardWebController(IGroupCardService groupCardService)
        {
            _groupCardService = groupCardService;
        }

        [HttpGet]
        [Route("groupCards-list")]
        public async Task<List<GroupCard>> GetAllGroupCards()
        {
            return await _groupCardService.GetAllGroupCards();
        }


        [HttpPost]
        [Route("create-groupCard")]
        public async Task<GroupCard?> CreateGroupCard([FromForm] CreateGroupCardModel createGroupCardModel)
        {
            return await _groupCardService.CreateGroupCard(createGroupCardModel);
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
            return Ok(groupCards);
        }
    }
}
