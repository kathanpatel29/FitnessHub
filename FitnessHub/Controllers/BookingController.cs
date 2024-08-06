using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FitnessHub.Models;

namespace FitnessHub.Controllers
{
    public class BookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Booking
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include("User").Include("DanceClass").Include("SwimmingLesson").ToList();
            return View(bookings);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            ViewBag.DanceClassID = new SelectList(db.DanceClasses, "ClassID", "Name");
            ViewBag.SwimmingLessonID = new SelectList(db.SwimmingLessons, "LessonID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,UserID,DanceClassID,SwimmingLessonID,BookingDate,AmountPaid,Status")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DanceClassID = new SelectList(db.DanceClasses, "ClassID", "Name", booking.DanceClassID);
            ViewBag.SwimmingLessonID = new SelectList(db.SwimmingLessons, "LessonID", "Name", booking.SwimmingLessonID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", booking.UserID);
            return View(booking);
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.DanceClassID = new SelectList(db.DanceClasses, "ClassID", "Name", booking.DanceClassID);
            ViewBag.SwimmingLessonID = new SelectList(db.SwimmingLessons, "LessonID", "Name", booking.SwimmingLessonID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", booking.UserID);
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,UserID,DanceClassID,SwimmingLessonID,BookingDate,AmountPaid,Status")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DanceClassID = new SelectList(db.DanceClasses, "ClassID", "Name", booking.DanceClassID);
            ViewBag.SwimmingLessonID = new SelectList(db.SwimmingLessons, "LessonID", "Name", booking.SwimmingLessonID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", booking.UserID);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
