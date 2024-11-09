using AutoMapper;
using Service.Model.BookingModel;
using TarotBooking.Mappers;
using TarotBooking.Model.BookingModel;
using TarotBooking.Model.NotificationModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
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
        private readonly IMapper _mapper;

        public BookingService(IBookingRepo bookingRepo, IBookingTopicRepo bookingTopicRepo, INotificationService notificationService, IUserRepo userRepo, IReaderRepo readerRepo, IMapper mapper)
        {
            _bookingRepo = bookingRepo;
            _bookingTopicRepo = bookingTopicRepo;
            _notificationService = notificationService;
            _userRepo = userRepo;
            _readerRepo = readerRepo;
            _mapper = mapper;
        }

        public async Task<Booking> GetBookingById (string bookingId)
        {
            var booking = await _bookingRepo.GetById(bookingId);


            if (booking == null)
            {
                throw new Exception("Booking not exist!");
            }
            return booking;
        }

        public async Task<Booking?> CreateBooking(CreateBookingModel createBookingModel)
        {
            var createBooking = createBookingModel.ToCreateBooking();

            if (createBooking == null)
                throw new Exception("Unable to create booking!");
            var reader = await _readerRepo.GetById(createBookingModel.ReaderId);
            createBooking.Total = reader.Price * (float)TinhSoGio(createBookingModel.TimeStart, createBookingModel.TimeEnd);

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
                Text = $"You have booking from {newBooking.TimeStart} to {newBooking.TimeEnd}"
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

            var updatedBooking =await _bookingRepo.Update(updateBooking); ;

            await RecalculateReaderRating(updatedBooking.ReaderId);

            return updatedBooking;
        }

        private async Task RecalculateReaderRating(string readerId)
        {
            // Get all bookings with feedback for this reader
            var bookingsWithFeedback = await _bookingRepo.GetBookingsByReaderId(readerId, null, null, null);
            var feedbackBookings = bookingsWithFeedback.Where(b => b.Feedback != null);

            if (!feedbackBookings.Any()) return;

            // Calculate the new average rating
            var totalRating = feedbackBookings.Sum(b => b.Rating ?? 0);
            var averageRating = (float)totalRating / feedbackBookings.Count();

            // Update the reader's rating
            var reader = await _readerRepo.GetById(readerId);
            if (reader != null)
            {
                reader.Rating = averageRating;
                await _readerRepo.Update(reader);
            }
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

            foreach (var booking in bookings)
            {
                var user = await _userRepo.GetById(booking.UserId);
                var feedbackModel = new FeedbackModel
                {
                    UserName = user?.Name ?? "Unknown",
                    Booking = _mapper.Map<BookingDto>(booking)
                };

                feedbacks.Add(feedbackModel);
            }
            //foreach (var booking in bookings.Where(b => b.Feedback != null))
            //{
            //    var user = await _userRepo.GetById(booking.UserId);
            //    var feedbackModel = new FeedbackModel
            //    {
            //        UserName = user?.Name ?? "Unknown",
            //        Booking = _mapper.Map<BookingDto>(booking)
            //    };

            //    feedbacks.Add(feedbackModel);
            //}

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

            foreach (var booking in bookings)
            {
                var user = await _readerRepo.GetById(booking.ReaderId);
                var feedbackModel = new FeedbackModel
                {
                    UserName = user?.Name ?? "Unknown",
                    Booking = _mapper.Map<BookingDto>(booking)
                };

                feedbacks.Add(feedbackModel);
            }

            return feedbacks;
        }

        public async Task<int> GetBookingCountToday(string readerId)
        {
            return await _bookingRepo.GetBookingCountToday(readerId);
        }

        public async Task<int> GetTotalBookingCount(string readerId)
        {
            return await _bookingRepo.GetTotalBookingCount(readerId);
        }

        public async Task<PagedBookingResult> GetPagedBookings(string readerId, int pageNumber, int pageSize, string? searchTerm = null, List<int>? statuses = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (statuses != null && statuses.Any(status => status < 1 || status > 4))
            {
                throw new ArgumentException("Status values must be between 1 and 4.");
            }

            var bookings = await _bookingRepo.GetBookingsByReaderId(readerId, statuses, startDate, endDate);

            var totalRecords = bookings.Count;
            var pagedBookings = bookings
                .OrderBy(b => b.TimeStart)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var pagedUserIds = pagedBookings.Select(b => b.UserId).Distinct().ToList();
            var bookingWithUserNames = new List<FeedbackModel>();

            foreach (var booking in pagedBookings)
            {
                // Lấy user cho từng booking bằng UserId
                var user = await _userRepo.GetById(booking.UserId);

                bookingWithUserNames.Add(new FeedbackModel
                {
                    Booking = _mapper.Map<BookingDto>(booking),
                    UserName = user?.Name ?? "Unknown" // Nếu không tìm thấy tên người dùng, trả về "Unknown"
                });
            }
            if (!string.IsNullOrEmpty(searchTerm))
            {
                bookingWithUserNames = bookingWithUserNames
                    .Where(b => b.UserName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return new PagedBookingResult
            {
                TotalRecords = totalRecords,
                Bookings = bookingWithUserNames
            };
        }

        public static decimal TinhSoGio(DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null || endTime == null)
            {
                throw new ArgumentNullException("Thời gian bắt đầu hoặc kết thúc không được null");
            }

            TimeSpan timeDifference = endTime.Value - startTime.Value;
            return (decimal)timeDifference.TotalHours;
        }

        public async Task<List<FeedbackModel>> GetBookingsWithFeedback(string readerId)
        {
            var bookings = await _bookingRepo.GetBookingsByReaderId(readerId,null,null,null);

            if (bookings == null || !bookings.Any())
                throw new Exception("No bookings found for the specified reader.");

            var feedbacksWithUserNames = new List<FeedbackModel>();

            foreach (var booking in bookings.Where(b => b.Feedback != null)) 
            {
                var user = await _userRepo.GetById(booking.UserId);

                var feedbackModel = new FeedbackModel
                {
                    UserName = user?.Name ?? "Unknown",
                    Booking = _mapper.Map<BookingDto>(booking)
                };

                feedbacksWithUserNames.Add(feedbackModel);
            }

            return feedbacksWithUserNames;
        }

        public async Task<List<TimeSlot>> GetAvailableTimeSlots(DateTime date, string readerId)
        {
            var businessStart = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0); 
            var businessEnd = new DateTime(date.Year, date.Month, date.Day, 17, 0, 0); 

            var bookings = await _bookingRepo.GetBookingsByReaderId(readerId, null, date, date.AddDays(1));

            var sortedBookings = bookings.OrderBy(b => b.TimeStart).ToList();

            var availableSlots = new List<TimeSlot>();

            if (!sortedBookings.Any() || sortedBookings.First().TimeStart > businessStart.AddMinutes(30))
            {
                var potentialEnd = sortedBookings.Any() ? sortedBookings.First().TimeStart.Value.AddMinutes(-30) : businessEnd;
                if ((potentialEnd - businessStart).TotalMinutes >= 30)
                {
                    availableSlots.Add(new TimeSlot
                    {
                        Start = businessStart,
                        End = potentialEnd
                    });
                }
            }

            for (int i = 0; i < sortedBookings.Count - 1; i++)
            {
                var endCurrent = (sortedBookings[i].TimeEnd ?? DateTime.MinValue).AddMinutes(30); 
                var startNext = (sortedBookings[i + 1].TimeStart ?? DateTime.MaxValue).AddMinutes(-30); 

                if ((startNext - endCurrent).TotalMinutes >= 30)
                {
                    availableSlots.Add(new TimeSlot
                    {
                        Start = endCurrent,
                        End = startNext
                    });
                }
            }

            if (sortedBookings.Any() && sortedBookings.Last().TimeEnd.HasValue && sortedBookings.Last().TimeEnd.Value.AddMinutes(30) < businessEnd)
            {
                var potentialStart = sortedBookings.Last().TimeEnd.Value.AddMinutes(30);
                if ((businessEnd - potentialStart).TotalMinutes >= 30)
                {
                    availableSlots.Add(new TimeSlot
                    {
                        Start = potentialStart,
                        End = businessEnd
                    });
                }
            }

            return availableSlots;
        }

        public async Task<List<Booking>> GetBookingsTodayByReaderIdAsync(string readerId)
        {
            var today = DateTime.Today;
            var bookings = await _bookingRepo.GetBookingsByReaderId(readerId, null, today, today.AddDays(1));

            return bookings;
        }


        public async Task<PagedBookingResult> GetPagedBookingsByDay(string readerId, int pageNumber, int pageSize)
        {

            var today = DateTime.Today;

            var bookings = await _bookingRepo.GetBookingsByReaderId(readerId, null, today, today.AddDays(1));

            var totalRecords = bookings.Count;
            var pagedBookings = bookings
                .OrderBy(b => b.TimeStart)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var pagedUserIds = pagedBookings.Select(b => b.UserId).Distinct().ToList();
            var bookingWithUserNames = new List<FeedbackModel>();

            foreach (var booking in pagedBookings)
            {
                var user = await _userRepo.GetById(booking.UserId);

                bookingWithUserNames.Add(new FeedbackModel
                {
                    Booking = _mapper.Map<BookingDto>(booking),
                    UserName = user?.Name ?? "Unknown" 
                });
            }

            return new PagedBookingResult
            {
                TotalRecords = totalRecords,
                Bookings = bookingWithUserNames
            };
        }


        public async Task<Booking> ChangeBookingStatus(string bookingId, int status)
        {
            var booking = await _bookingRepo.GetById(bookingId);

            if (booking == null)
                throw new Exception("Booking not found!");

             booking.Status = status;

            var updatedBooking = await _bookingRepo.Update(booking);

            return updatedBooking;
        }


    }
}
