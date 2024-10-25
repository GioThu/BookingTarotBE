using Microsoft.EntityFrameworkCore;
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

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }


        public async Task<bool> DeleteUser(string cateId)
        {
            var user = await _userRepo.GetById(cateId);

            if (user == null) throw new Exception("Unable to find user!");

            return await _userRepo.Delete(cateId);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var user = await _userRepo.GetAll();

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
                user = user,
                url = images.Select(img => img.Url).ToList()
            };
        }

        public async Task<List<User>> GetPagedUsersAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var users = await _userRepo.GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(u => u.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                             .ToList();
            }

            var pagedUsers = users.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            return pagedUsers;
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
    }
}
