using AutoMapper; // Import AutoMapper
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Model.BookingModel;
using TarotBooking.Model.BookingModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingWebController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingWebController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper; 
        }

        [HttpGet]
        [Route("bookings-list")]
        public async Task<List<BookingDto>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookings();
            return _mapper.Map<List<BookingDto>>(bookings); 
        }

        [HttpPost]
        [Route("create-booking")]
        public async Task<BookingDto?> CreateBooking([FromForm] CreateBookingModel createBookingModel)
        {
            var booking = await _bookingService.CreateBooking(createBookingModel);
            return _mapper.Map<BookingDto>(booking);
        }

        [HttpPost]
        [Route("create-feedback")]
        public async Task<IActionResult> CreateFeedback([FromForm] CreateFeedbackModel createFeedbackModel)
        {
            var feedback = await _bookingService.CreateFeedback(createFeedbackModel);
            return Ok(feedback); 
        }

        [HttpGet("GetBookingsByReaderId/{readerId}")]
        public async Task<IActionResult> GetBookingsByReaderId(string readerId, int pageNumber = 1, int pageSize = 10)
        {
            var bookings = await _bookingService.GetBookingsByReaderIdAsync(readerId, pageNumber, pageSize);
            return Ok(bookings); 
        }

        [HttpGet("GetBookingsByUserId/{userId}")]
        public async Task<IActionResult> GetBookingsByUserId(string userId, int pageNumber = 1, int pageSize = 10)
        {
            var bookings = await _bookingService.GetBookingsByUserIdAsync(userId, pageNumber, pageSize);
            return Ok(bookings); 
        }

        [HttpGet("booking-count-today")]
        public async Task<IActionResult> GetBookingCountToday(string readerId)
        {
            var count = await _bookingService.GetBookingCountToday(readerId);
            return Ok(count); 
        }

        [HttpGet("total-booking-count")]
        public async Task<IActionResult> GetTotalBookingCount(string readerId)
        {
            var count = await _bookingService.GetTotalBookingCount(readerId);
            return Ok(count); 
        }

        [HttpGet("paged-bookings")]
        public async Task<IActionResult> GetPagedBookings(string readerId, int pageNumber = 1, int pageSize = 10, string? searchTerm = null, [FromQuery] List<int>? statuses = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var result = await _bookingService.GetPagedBookings(readerId, pageNumber, pageSize, searchTerm, statuses, startDate, endDate);
            return Ok(result); 
        }

        [HttpGet("get-feed-back")]
        public async Task<IActionResult> GetBookingFeedBackByReaderId(string readerId)
        {
            var feedbacks = await _bookingService.GetBookingsWithFeedback(readerId);
            return Ok(feedbacks);
        }

        [HttpGet("available-time-slots")]
        public async Task<IActionResult> GetAvailableTimeSlots(string readerId, DateTime date)
        {
            var availableSlots = await _bookingService.GetAvailableTimeSlots(date, readerId);
            return Ok(availableSlots);
        }


    }
}
