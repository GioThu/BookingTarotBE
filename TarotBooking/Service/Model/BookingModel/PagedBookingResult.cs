using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarotBooking.Model.BookingModel;
using TarotBooking.Models;

namespace Service.Model.BookingModel
{
    public class PagedBookingResult
    {
        public int TotalRecords { get; set; }
        public List<FeedbackModel> Bookings { get; set; }
    }
}
