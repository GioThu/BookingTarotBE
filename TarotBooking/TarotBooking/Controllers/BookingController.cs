using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public BookingWebController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("bookings-list")]
        public async Task<List<Booking>> GetAllBookings()
        {
            return await _bookingService.GetAllBookings();
        }


        [HttpPost]
        [Route("create-booking")]
        public async Task<Booking?> CreateBooking([FromForm] CreateBookingModel createBookingModel)
        {
            return await _bookingService.CreateBooking(createBookingModel);
        }

        [HttpPost]
        [Route("create-feedback")]
        public async Task<Booking?> CreateBooking([FromForm] CreateFeedbackModel createFeedbackModel)
        {
            return await _bookingService.CreateFeedback(createFeedbackModel);
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


    }
}
