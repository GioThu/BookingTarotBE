using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Model.ReaderTopicModel;
using TarotBooking.Models;
using TarotBooking.Services.Implementations;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderWebController : ControllerBase
    {
        private readonly IReaderService _readerService;
        private readonly IReaderTopicService _readerTopicService;
        public ReaderWebController(IReaderService readerService, IReaderTopicService readerTopicService)
        {
            _readerService = readerService;
            _readerTopicService = readerTopicService;
        }

        [HttpGet]
        [Route("readers-list")]
        public async Task<List<Reader>> GetAllReaders()
        {
            return await _readerService.GetAllReaders();
        }


        [HttpPost]
        [Route("update-reader")]
        public async Task<Reader?> UpdateReader([FromForm] UpdateReaderModel updateReaderModel)
        {
            return await _readerService.UpdateReader(updateReaderModel);
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
                return Ok(readerWithImages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("reader-topic/{readerId}")]
        public async Task<IActionResult> GetReaderTopic( string readerId, int pageNumber = 1, int pageSize = 10)
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

            return Ok(readers);
        }


    }
}
