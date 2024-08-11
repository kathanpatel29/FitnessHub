using System.Linq;
using System.Net;
using System.Web.Mvc;
using FitnessHub.Models;
using System.Data.Entity;

namespace FitnessHub.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.User).Include(b => b.DanceClass).Include(b => b.SwimmingLesson).ToList();
            return View(bookings);
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int id)
        {
            var booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName");
            ViewBag.DanceClassID = new SelectList(db.DanceClasses, "DanceClassID", "Name");
            ViewBag.SwimmingLessonID = new SelectList(db.SwimmingLessons, "SwimmingLessonID", "Name");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", booking.UserID);
            ViewBag.DanceClassID = new SelectList(db.DanceClasses, "DanceClassID", "Name", booking.DanceClassID);
            ViewBag.SwimmingLessonID = new SelectList(db.SwimmingLessons, "SwimmingLessonID", "Name", booking.SwimmingLessonID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int id)
        {
            var booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", booking.UserID);
            ViewBag.DanceClassID = new SelectList(db.DanceClasses, "DanceClassID", "Name", booking.DanceClassID);
            ViewBag.SwimmingLessonID = new SelectList(db.SwimmingLessons, "SwimmingLessonID", "Name", booking.SwimmingLessonID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", booking.UserID);
            ViewBag.DanceClassID = new SelectList(db.DanceClasses, "DanceClassID", "Name", booking.DanceClassID);
            ViewBag.SwimmingLessonID = new SelectList(db.SwimmingLessons, "SwimmingLessonID", "Name", booking.SwimmingLessonID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int id)
        {
            var booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
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
