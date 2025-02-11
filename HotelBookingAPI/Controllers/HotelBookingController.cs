using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;

        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }

        // Create/Edit
        [HttpPost]
        public ActionResult<HotelBooking> CreateEdit(HotelBooking booking)
        {
            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookingInDb = _context.Bookings.Find(booking.Id);

                if (bookingInDb == null)
                    return NotFound();

                _context.Entry(bookingInDb).CurrentValues.SetValues(booking);
            }

            _context.SaveChanges();

            return Ok(booking); // Return 200 OK with the booking object
        }

        // Get
        [HttpGet]
        public ActionResult<HotelBooking> Get(int id)
        {
            var result = _context.Bookings.Find(id);

            if (result == null)
                return NotFound();

            return Ok(result); // Return 200 OK with the result
        }

        // Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _context.Bookings.Find(id);

            if (result == null)
                return NotFound();

            _context.Bookings.Remove(result);
            _context.SaveChanges();

            return NoContent(); // Return 204 No Content after deletion
        }

        // Get all
        [HttpGet]
        public ActionResult<IEnumerable<HotelBooking>> GetAll()
        {
            var result = _context.Bookings.ToList();
            return Ok(result); // Return 200 OK with the list of bookings
        }
    }
}
