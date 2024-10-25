    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.UserModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWebController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserWebController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("users-list")]
        public async Task<List<User>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }


        [HttpPost]
        [Route("update-user")]
        public async Task<User?> UpdateUser([FromForm] UpdateUserModel updateUserModel)
        {
            return await _userService.UpdateUser(updateUserModel);
        }

        [HttpPost]
        [Route("delete-user")]
        public async Task<bool> DeleteUser([FromForm] User user)
        {
            return await _userService.DeleteUser(user.Id);
        }

        [HttpGet]
        [Route("user-with-images/{userId}")]
        public async Task<ActionResult<UserWithImageModel>> GetUserWithImageById(string userId)
        {
            try
            {
                var userWithImages = await _userService.GetUserWithImageById(userId);
                if (userWithImages == null) return NotFound("User not found.");
                return Ok(userWithImages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPagedUsers")]
        public async Task<IActionResult> GetPagedUsers(int pageNumber = 1, int pageSize = 10, string? searchTerm = "")
        {
            var users = await _userService.GetPagedUsersAsync(pageNumber, pageSize, searchTerm);

            return Ok(users);
        }

 

    }
}
