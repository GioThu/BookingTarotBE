using TarotBooking.Mappers;
using TarotBooking.Model.GroupCardModel;
using TarotBooking.Model.ImageModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class GroupCardService : IGroupCardService
    {
        private readonly IGroupCardRepo _groupCardRepo;
        private readonly IImageService _imageService;
        private readonly IUserGroupCardRepo _userGroupCardRepo;

        public GroupCardService(IGroupCardRepo groupCardRepo, IUserGroupCardRepo userGroupCardRepo, IImageService imageService)
        {
            _groupCardRepo = groupCardRepo;
            _imageService = imageService;
            _userGroupCardRepo = userGroupCardRepo;
        }

        public async Task<GroupCard?> CreateGroupCard(CreateGroupCardModel createGroupCardModel)
        {
            var createGroupCard = createGroupCardModel.ToCreateGroupCard();
            if (createGroupCardModel.ReaderId != null)
            {
                createGroupCard.IsPublic = false;
            }
            else
            {
                createGroupCard.IsPublic = true;
            }
            if (createGroupCard == null)
                throw new Exception("Unable to create groupCard!");

            var newGroupCard = await _groupCardRepo.Add(createGroupCard);

            var createImageModel = new CreateImageModel
            {
                GroupId = newGroupCard.Id,
                File = createGroupCardModel.image
            };
            await _imageService.CreateImage(createImageModel);

            if (createGroupCardModel.ReaderId != null)
            {
                var userGroupCard = GroupCardMappers.ToCreateUserGroupCard(newGroupCard.Id, createGroupCardModel.ReaderId);
                await _userGroupCardRepo.Add(userGroupCard);
            }

            return newGroupCard;
        }


        public async Task<bool> DeleteGroupCard(string groupCardId)
        {
            var groupCard = await _groupCardRepo.GetById(groupCardId);

            if (groupCard == null) throw new Exception("Unable to find groupCard!");

            return await _groupCardRepo.Delete(groupCardId);
        }

        public async Task<List<GroupCard>> GetAllGroupCards()
        {
            var groupCard = await _groupCardRepo.GetAll();

            if (groupCard == null) throw new Exception("No groupCard in stock!");

            return groupCard.ToList();
        }

        public async Task<GroupCard?> UpdateGroupCard(UpdateGroupCardModel updateGroupCardModel)
        {
            var groupCard = await _groupCardRepo.GetById(updateGroupCardModel.Id);

            if (groupCard == null) throw new Exception("Unable to find groupCard!");

            var updateGroupCard = updateGroupCardModel.ToUpdateGroupCard();

            if (updateGroupCard == null) throw new Exception("Unable to update groupCard!");

            return await _groupCardRepo.Update(updateGroupCard);
        }

        public async Task<List<GroupCard>> GetGroupCardsByReaderIdAsync(string readerId, int pageNumber, int pageSize)
        {
            var groupCards = await _groupCardRepo.GetGroupCardsByReaderIdAsync(readerId, pageNumber, pageSize);
            return groupCards;
        }

    }
}
