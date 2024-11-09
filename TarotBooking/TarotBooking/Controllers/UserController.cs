using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.UserModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;
using AutoMapper;
using Service.Model.UserModel;
using Microsoft.AspNetCore.Authorization;
using Service.Model.ReaderModel;
using TarotBooking.Services.Implementations;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWebController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserWebController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("users-list")]
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return _mapper.Map<List<UserDto>>(users);
        }

        [HttpPost]
        [Route("update-user")]
        public async Task<UserDto?> UpdateUser([FromForm] UpdateUserModel updateUserModel)
        {
            var user = await _userService.UpdateUser(updateUserModel);
            return _mapper.Map<UserDto>(user);
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

        [HttpPost]
        [Route("change-user-status")]
        public async Task<bool> ChangeReaderStatus(string userId)
        {
            var user = await _userService.ChangeStatus(userId);
            return user;
        }
    }
 }
