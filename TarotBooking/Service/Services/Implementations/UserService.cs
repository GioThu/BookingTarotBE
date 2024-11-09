using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Model.UserModel;
using TarotBooking.Mappers;
using TarotBooking.Model.UserModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Repository.Implementations;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }


        public async Task<bool> DeleteUser(string cateId)
        {
            var user = await _userRepo.GetById(cateId);

            if (user == null) throw new Exception("Unable to find user!");

            return await _userRepo.Delete(cateId);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var user = await _userRepo.GetAllBlocked();

            if (user == null) throw new Exception("No user in stock!");

            return user.ToList();
        }

        public async Task<User?> UpdateUser(UpdateUserModel updateUserModel)
        {
            var user = await _userRepo.GetById(updateUserModel.Id);

            if (user == null) throw new Exception("Unable to find user!");

            var updateUser = updateUserModel.ToUpdateUser();

            if (updateUser == null) throw new Exception("Unable to update user!");

            return await _userRepo.Update(updateUser);
        }

        public async Task<UserWithImageModel?> GetUserWithImageById(string userId)
        {
            var user = await _userRepo.GetById(userId);
            if (user == null) throw new Exception("User not found!");

            var images = await _userRepo.GetUserImagesById(userId);

            return new UserWithImageModel
            {
                user = _mapper.Map<UserDto>(user),
                url = images.Select(img => img.Url).ToList()
            };
        }

        public async Task<PagedUserModel> GetPagedUsersAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var users = await _userRepo.GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(u => u.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                             .ToList();
            }

            var totalItems = users.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Pagination
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var pagedUsers = users.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();
            var userDtos = _mapper.Map<List<UserDto>>(pagedUsers);

            return new PagedUserModel
            {
                Users = userDtos,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<User?> CreateUser(string fullname, string email)
        {
            var createTopic = UserMappers.ToCreateUser(fullname, email);

            if (createTopic == null) throw new Exception("Unable to create topic!");

            return await _userRepo.Add(createTopic);
        }

        public async Task<User?> GetUserWithEmail(string email)
        {
            var user = await _userRepo.GetByEmail(email);
            if (user == null) return null;
            return await _userRepo.GetByEmail(email);
        }

        public async Task<bool> ChangeStatus(string userId)
        {
            var user = await _userRepo.GetById(userId);

            if (user == null) throw new Exception("User not found!");

            // Toggle the status
            user.Status = user.Status == "Active" ? "Blocked" : "Active";

            await _userRepo.Update(user);
            return true;
        }
    }
}
