using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationWebController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationWebController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("get-reader-noti")]
        public async Task<IActionResult> GetNotificationsByReaderId(string readerId)
        {

            var notifications = await _notificationService.GetNotificationsByReaderId(readerId);

            return Ok(notifications);
        }

        [HttpGet]
        [Route("get-user-noti")]
        public async Task<IActionResult> GetNotificationsByUserId(string userId)
        {

            var notifications = await _notificationService.GetNotificationsByUserId(userId);

            return Ok(notifications);
        }
    }
}
