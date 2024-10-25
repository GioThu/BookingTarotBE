using TarotBooking.Mappers;
using TarotBooking.Model.BookingModel;
using TarotBooking.Model.BookingModel;
using TarotBooking.Model.ImageModel;
using TarotBooking.Model.NotificationModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Repository.Implementations;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly IBookingTopicRepo _bookingTopicRepo;
        private readonly INotificationService _notificationService;
        private readonly IUserRepo _userRepo;
        private readonly IReaderRepo _readerRepo;

        public BookingService(IBookingRepo bookingRepo, IBookingTopicRepo bookingTopicRepo, INotificationService notificationService, IUserRepo userRepo, IReaderRepo readerRepo)
        {
            _bookingRepo = bookingRepo;
            _bookingTopicRepo = bookingTopicRepo;
            _notificationService = notificationService;
            _userRepo = userRepo;
            _readerRepo = readerRepo;
        }

        public async Task<Booking?> CreateBooking(CreateBookingModel createBookingModel)
        {
            var createBooking = createBookingModel.ToCreateBooking();

            if (createBooking == null)
                throw new Exception("Unable to create booking!");
            
            var newBooking = await _bookingRepo.Add(createBooking);
            if (createBookingModel.ListTopicId != null)
                foreach (var topic in createBookingModel.ListTopicId)
                {
                    BookingTopic bookingTopic = BookingMappers.ToCreateBookingTopic(newBooking.Id, topic);
                    if (bookingTopic == null)
                    {
                        throw new Exception("Unable to create bookingTopic!");

                    }
                    await _bookingTopicRepo.Add(bookingTopic);
                }
            CreateNotificationModel createNotification = new CreateNotificationModel
            {
                ReaderId = createBookingModel.ReaderId,
                Text = $"Bạn có một booking từ {newBooking.TimeStart} đến {newBooking.TimeEnd}"
            };
            await _notificationService.CreateNotification(createNotification);

            return newBooking;
            
        }

        public async Task<Booking?> CreateFeedback(CreateFeedbackModel createFeedbackModel)
        {
            var booking = await _bookingRepo.GetById(createFeedbackModel.BookingId);

            if (booking == null) throw new Exception("Unable to find booking!");

            var updateBooking = createFeedbackModel.ToCreateFeedback();

            if (updateBooking == null) throw new Exception("Unable to update booking!");

            return await _bookingRepo.Update(updateBooking);
        }

        public async Task<bool> DeleteBooking(string bookingId)
        {
            var booking = await _bookingRepo.GetById(bookingId);

            if (booking == null) throw new Exception("Unable to find booking!");

            return await _bookingRepo.Delete(bookingId);
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            var booking = await _bookingRepo.GetAll();

            if (booking == null) throw new Exception("No booking in stock!");

            return booking.ToList();
        }

        //public async Task<Booking?> UpdateBooking(UpdateBookingModel updateBookingModel)
        //{
        //    var booking = await _bookingRepo.GetById(updateBookingModel.Id);

        //    if (booking == null) throw new Exception("Unable to find booking!");

        //    var updateBooking = updateBookingModel.ToUpdateBooking();

        //    if (updateBooking == null) throw new Exception("Unable to update booking!");

        //    return await _bookingRepo.Update(updateBooking);
        //}

        public async Task<List<FeedbackModel>> GetBookingsByReaderIdAsync(string readerId, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Page number and page size must be greater than zero.");

            var bookings = await _bookingRepo.GetBookingsByReaderIdAsync(readerId, pageNumber, pageSize);

            if (bookings == null || !bookings.Any())
                throw new Exception("No feedback found for the specified reader.");

            var feedbacks = new List<FeedbackModel>();

            foreach (var booking in bookings.Where(b => b.Feedback != null))
            {
                var user = await _userRepo.GetById(booking.UserId);
                var feedbackModel = new FeedbackModel
                {
                    UserName = user?.Name ?? "Unknown",
                    Booking = booking
                };

                feedbacks.Add(feedbackModel);
            }

            return feedbacks;
        }

        public async Task<List<FeedbackModel>> GetBookingsByUserIdAsync(string userId, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Page number and page size must be greater than zero.");

            var bookings = await _bookingRepo.GetBookingsByUserIdAsync(userId, pageNumber, pageSize);

            if (bookings == null || !bookings.Any())
                throw new Exception("No feedback found for the specified reader.");

            var feedbacks = new List<FeedbackModel>();

            foreach (var booking in bookings.Where(b => b.Feedback != null))
            {
                var user = await _readerRepo.GetById(booking.ReaderId);
                var feedbackModel = new FeedbackModel
                {
                    UserName = user?.Name ?? "Unknown",
                    Booking = booking
                };

                feedbacks.Add(feedbackModel);
            }

            return feedbacks;
        }

    }
}
