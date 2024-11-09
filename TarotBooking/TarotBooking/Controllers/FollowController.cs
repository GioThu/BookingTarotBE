using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;
using AutoMapper;
using Service.Model.FollowModel;
using Microsoft.AspNetCore.Authorization;
using BE.Attributes;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowWebController : ControllerBase
    {
        private readonly IFollowService _followService;
        private readonly IMapper _mapper;

        public FollowWebController(IFollowService followService, IMapper mapper)
        {
            _followService = followService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("follows-list")]
        public async Task<List<FollowDto>> GetAllFollows()
        {
            var follows = await _followService.GetAllFollows();
            return _mapper.Map<List<FollowDto>>(follows);
        }


        [HttpPost]
        [Route("create-follow")]
        public async Task<FollowDto?> CreateFollow([FromForm] CreateFollowModel createFollowModel)
        {
            var follow = await _followService.CreateFollow(createFollowModel);
            return _mapper.Map<FollowDto>(follow);
        }

        [HttpPost]
        [Route("delete-follow")]
        public async Task<IActionResult> DeleteFollow([FromQuery] string userId, [FromQuery] string readerId)
        {
            try
            {
                var result = await _followService.DeleteFollowByUserAndReaderId(userId, readerId);
                if (result)
                    return Ok(new { message = "Follow deleted successfully." });
                else
                    return NotFound(new { message = "Follow not found for the provided user and reader IDs." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-followed")]
        public async Task<IActionResult> GetFollowed(string userId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var pagedReaders = await _followService.GetPagedReadersWithImageByUserId(userId, pageNumber, pageSize);
                return Ok(pagedReaders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
