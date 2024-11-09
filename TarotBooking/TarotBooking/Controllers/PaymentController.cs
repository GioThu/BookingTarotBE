using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Linq;
using System.Threading.Tasks;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly IBookingService _bookingService;


        public PaymentController(PaymentService paymentService, IBookingService bookingService)
        {
            _paymentService = paymentService;
            _bookingService = bookingService;
        }

        [HttpGet("create-payment")]
        public async Task<IActionResult> CreatePayment(string bookingId)
        {
            try {
                var booking =await _bookingService.GetBookingById(bookingId);


                string returnUrl = $"https://www.bookingtarot.somee.com/api/payment/payment-success?bookingId={bookingId}";
                //string returnUrl = $"https://localhost:7218/api/payment/payment-success?bookingId={bookingId}";

                string cancelUrl = "https://www.bookingtarot.somee.com/api/payment/payment-cancel";
                //string cancelUrl = "https://localhost:7218/api/payment/payment-cancel";
                var payment = _paymentService.CreatePayment((float)booking.Total, returnUrl, cancelUrl);
                var approvalUrl = payment.links.FirstOrDefault(lnk => lnk.rel.Equals("approval_url", System.StringComparison.OrdinalIgnoreCase))?.href;

                if (string.IsNullOrEmpty(approvalUrl))
                {
                    return BadRequest(new { Message = "Error generating PayPal approval URL." });
                }

                return Ok(new { approvalUrl });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpGet("payment-success")]
        public async Task<IActionResult> PaymentSuccess( string bookingId)
        {
            if (string.IsNullOrWhiteSpace(bookingId))
            {
                return BadRequest(new { Message = "Booking ID is required." });
            }

            var booking = await _bookingService.GetBookingById(bookingId);
            if (booking == null)
            {
                return NotFound(new { Message = "Booking not found." });
            }

            try
            {
               var newBooking= await _bookingService.ChangeBookingStatus(bookingId, 1);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the booking status.", Error = ex.Message });
            }

            var successPageUrl = "http://localhost:5173/my-booking";
            return Redirect(successPageUrl);
        }


        [HttpGet("payment-cancel")]
        public IActionResult PaymentCancel()
        {
            var successPageUrl = "http://localhost:5173/my-booking";
            return Redirect(successPageUrl);
        }
    }
}
