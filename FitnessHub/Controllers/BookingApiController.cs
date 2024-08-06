using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FitnessHub.Models;

namespace FitnessHub.Controllers.Api
{
    public class BookingApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<BookingDto> GetBookings()
        {
            return db.Bookings
                .Select(b => new BookingDto
                {
                    BookingID = b.BookingID,
                    DanceClassID = b.DanceClassID,
                    SwimmingLessonID = b.SwimmingLessonID,
                    UserID = b.UserID,
                    BookingDate = b.BookingDate,
                    AmountPaid = b.AmountPaid
                }).ToList();
        }

        [HttpGet]
        public IHttpActionResult GetBooking(int id)
        {
            var booking = db.Bookings
                .Select(b => new BookingDto
                {
                    BookingID = b.BookingID,
                    DanceClassID = b.DanceClassID,
                    SwimmingLessonID = b.SwimmingLessonID,
                    UserID = b.UserID,
                    BookingDate = b.BookingDate,
                    AmountPaid = b.AmountPaid
                }).FirstOrDefault(b => b.BookingID == id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpPost]
        public IHttpActionResult CreateBooking(BookingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var booking = new Booking
            {
                DanceClassID = dto.DanceClassID,
                SwimmingLessonID = dto.SwimmingLessonID,
                UserID = dto.UserID,
                BookingDate = dto.BookingDate,
                AmountPaid = dto.AmountPaid
            };

            db.Bookings.Add(booking);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = booking.BookingID }, dto);
        }

        [HttpPut]
        public IHttpActionResult UpdateBooking(int id, BookingDto dto)
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

            booking.DanceClassID = dto.DanceClassID;
            booking.SwimmingLessonID = dto.SwimmingLessonID;
            booking.UserID = dto.UserID;
            booking.BookingDate = dto.BookingDate;
            booking.AmountPaid = dto.AmountPaid;

            db.SaveChanges();

            return Ok(dto);
        }

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

            return Ok();
        }
    }
}
