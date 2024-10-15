using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Models;

namespace TarotBooking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class TestApi : ControllerBase
    {
        

        private readonly ILogger<TestApi> _logger;

        private readonly BookingTarotContext _context;
        public TestApi(BookingTarotContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
           return Ok(_context.Users.ToList());
        }

        
    }

    
}
