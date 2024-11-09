using AutoMapper;
using TarotBooking.Models;
using Service.Model.ReaderModel;
using Service.Model.TopicModel;
using Service.Model.UserModel;
using Service.Model.BookingModel;
using Service.Model.CommentModel;
using Service.Model.GroupCardModel;
using Service.Model.ImageModel;
using Service.Model.NotificationModel;
using Service.Model.PostModel;
using Service.Model.ReaderTopicModel;
using Service.Model.FollowModel;
using Service.Model.CardModel;

namespace TarotBooking.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Reader, ReaderDto>();
            CreateMap<Topic, TopicDto>();
            CreateMap<Booking, BookingDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<GroupCard, GroupCardDto>();
            CreateMap<Image, ImageDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<Post, PostDto>();
            CreateMap<ReaderTopic, ReaderTopicDto>();
            CreateMap<Follow, FollowDto>();
            CreateMap<Card, CardDto>();

        }
    }
}
