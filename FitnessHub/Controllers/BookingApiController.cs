using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FitnessHub.Models;
using System.Data.Entity;

namespace FitnessHub.Controllers.Api
{
    [Authorize]
    public class BookingApiController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BookingApi
        [HttpGet]
        public IHttpActionResult GetBookings()
        {
            var bookings = db.Bookings.Include(b => b.User).Include(b => b.DanceClass).Include(b => b.SwimmingLesson).ToList();
            return Ok(bookings);
        }

        // GET: api/BookingApi/5
        [HttpGet]
        public IHttpActionResult GetBooking(int id)
        {
            var booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // POST: api/BookingApi
        [HttpPost]
        public IHttpActionResult CreateBooking(BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var booking = new Booking
            {
                BookingID = bookingDto.BookingID,
                UserID = bookingDto.UserID,
                DanceClassID = bookingDto.DanceClassID,
                SwimmingLessonID = bookingDto.SwimmingLessonID,
                BookingDate = bookingDto.BookingDate,
                AmountPaid = bookingDto.AmountPaid,
                Status = bookingDto.Status
            };

            db.Bookings.Add(booking);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = booking.BookingID }, booking);
        }

        // PUT: api/BookingApi/5
        [HttpPut]
        public IHttpActionResult UpdateBooking(int id, BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            booking.UserID = bookingDto.UserID;
            booking.DanceClassID = bookingDto.DanceClassID;
            booking.SwimmingLessonID = bookingDto.SwimmingLessonID;
            booking.BookingDate = bookingDto.BookingDate;
            booking.AmountPaid = bookingDto.AmountPaid;
            booking.Status = bookingDto.Status;

            db.Entry(booking).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/BookingApi/5
        [HttpDelete]
        public IHttpActionResult DeleteBooking(int id)
        {
            var booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            db.Bookings.Remove(booking);
            db.SaveChanges();

            return Ok(booking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
