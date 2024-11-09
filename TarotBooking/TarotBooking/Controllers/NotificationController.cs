using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Services.Interfaces;
using AutoMapper;
using TarotBooking.Model.NotificationModel;
using Service.Model.NotificationModel;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationWebController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationWebController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-reader-noti")]
        public async Task<IActionResult> GetNotificationsByReaderId(string readerId)
        {
            var notifications = await _notificationService.GetNotificationsByReaderId(readerId);
            var result = _mapper.Map<List<NotificationDto>>(notifications);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-user-noti")]
        public async Task<IActionResult> GetNotificationsByUserId(string userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserId(userId);
            var result = _mapper.Map<List<NotificationDto>>(notifications);
            return Ok(result);
        }

        [HttpPost]
        [Route("mark-as-read")]
        public async Task<IActionResult> MarkNotificationAsRead(string notificationId)
        {
            try
            {
                await _notificationService.MarkNotificationAsRead(notificationId);
                return Ok(new { Message = "Notification marked as read successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("get-reader-noti-counts")]
        public async Task<IActionResult> GetNotificationCountsByReaderId(string readerId)
        {
            var (total, unread) = await _notificationService.GetNotificationCountsByReaderId(readerId);
            var result = new NotificationCountsDto
            {
                Total = total,
                Unread = unread
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("get-user-noti-counts")]
        public async Task<IActionResult> GetNotificationCountsByUserId(string userId)
        {
            var (total, unread) = await _notificationService.GetNotificationCountsByUserId(userId);
            var result = new NotificationCountsDto
            {
                Total = total,
                Unread = unread
            };
            return Ok(result);
        }
    }
}
