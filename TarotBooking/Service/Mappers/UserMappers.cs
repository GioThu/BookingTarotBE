using TarotBooking.Model.UserModel;
using TarotBooking.Models;
namespace TarotBooking.Mappers
{
    public static class UserMappers
    {
        public static User ToCreateUser(string fullname, string email)
        {
            return new User
            {
                Id = Utils.Utils.GenerateIdModel("User"),
                Name = fullname,
                Email = email,
                Role = 1,
                Status ="Active"
            };
        }

        public static User ToUpdateUser(this UpdateUserModel updateUserModel)
        {
            return new User
            {
                 Id = updateUserModel.Id,
                 Name= updateUserModel.Name,
                 Phone= updateUserModel.Phone,
                 Status= updateUserModel.Status,
                 Description= updateUserModel.Description,
                 Dob = updateUserModel.Dob
            };
        }
    }
}
