using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Model.ReaderTopicModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;
using AutoMapper;
using Service.Model.ReaderModel;
using Service.Model.FollowModel;
using TarotBooking.Services.Implementations;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderWebController : ControllerBase
    {
        private readonly IReaderService _readerService;
        private readonly IReaderTopicService _readerTopicService;
        private readonly IMapper _mapper;

        public ReaderWebController(IReaderService readerService, IReaderTopicService readerTopicService, IMapper mapper)
        {
            _readerService = readerService;
            _readerTopicService = readerTopicService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("readers-list")]
        public async Task<List<ReaderDto>> GetAllReaders()
        {
            var readers = await _readerService.GetAllReaders();
            return _mapper.Map<List<ReaderDto>>(readers);
        }

        [HttpPost]
        [Route("update-reader")]
        public async Task<ReaderDto?> UpdateReader([FromForm] UpdateReaderModel updateReaderModel)
        {
            var reader = await _readerService.UpdateReader(updateReaderModel);
            return _mapper.Map<ReaderDto>(reader);
        }

        [HttpPost]
        [Route("delete-reader")]
        public async Task<bool> DeleteReader([FromForm] Reader reader)
        {
            return await _readerService.DeleteReader(reader.Id);
        }

        [HttpGet]
        [Route("reader-with-images/{readerId}")]
        public async Task<ActionResult<ReaderWithImageModel>> GetReaderWithImageById(string readerId)
        {
            try
            {
                var readerWithImages = await _readerService.GetReaderWithImageById(readerId);
                if (readerWithImages == null) return NotFound("Reader not found.");
                return readerWithImages;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("reader-topic/{readerId}")]
        public async Task<IActionResult> GetReaderTopic(string readerId, int pageNumber = 1, int pageSize = 10)
        {
            var topics = await _readerTopicService.GetTopicsByReaderIdAsync(readerId, pageNumber, pageSize);
            return Ok(topics);
        }

        [HttpPost]
        [Route("create-reader-topic")]
        public async Task<IActionResult> CreateReaderTopic([FromBody] CreateReaderTopicModel createReaderTopicModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var createdTopic = await _readerTopicService.CreateReaderTopic(createReaderTopicModel);

                if (createdTopic == null)
                {
                    return BadRequest("Failed to create the reader topic.");
                }

                return Ok(createdTopic);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating reader topic: {ex.Message}");
            }
        }

        [HttpGet("GetPagedReaders")]
        public async Task<IActionResult> GetPagedReaders(int pageNumber = 1, int pageSize = 10, string? searchTerm = "")
        {
            var readers = await _readerService.GetPagedReadersAsync(pageNumber, pageSize, searchTerm);
            var result = _mapper.Map<List<ReaderDto>>(readers);
            return Ok(result);
        }

        [HttpGet("GetPagedReadersInfo")]
        public async Task<IActionResult> GetPagedReadersInfo(
            [FromQuery] string? readerName = null,
            [FromQuery] float? minPrice = null,
            [FromQuery] float? maxPrice = null,
            [FromQuery] List<string>? topicIds = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var readersInfo = await _readerService.GetPagedReadersInfoAsync(readerName, minPrice, maxPrice, topicIds, pageNumber, pageSize);
            return Ok(readersInfo);
        }

        [HttpGet]
        [Route("active-readers-count")]
        public async Task<IActionResult> GetActiveReadersCount()
        {
            try
            {
                var activeReaderCount = await _readerService.CountActiveReaders();
                return Ok(activeReaderCount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching active readers count: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("create-reader")]
        public async Task<ReaderDto?> CreateFollow([FromForm] CreateReaderModel createReaderModel)
        {
            var reader = await _readerService.CreateReader(createReaderModel);
            return _mapper.Map<ReaderDto>(reader);
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordModel changePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var result = await _readerService.ChangePassword(changePasswordModel);

                if (!result)
                {
                    return BadRequest("Old password is incorrect or password change failed.");
                }

                return Ok("Password changed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error changing password: {ex.Message}");
            }
        }

        
        [HttpPost]
        [Route("change-reader-status")]
        public async Task<bool> ChangeReaderStatus(string readerId)
        {
            var reader = await _readerService.ChangeStatus(readerId);
            return reader;
        }

    }
}
