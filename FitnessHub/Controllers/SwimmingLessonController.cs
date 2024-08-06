using System.Linq;
using System.Web.Mvc;
using FitnessHub.Models;

namespace FitnessHub.Controllers
{
    public class SwimmingLessonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SwimmingLesson
        public ActionResult Index(string search = "")
        {
            var lessons = db.SwimmingLessons
                .Where(l => l.Name.Contains(search) || l.Instructor.Contains(search))
                .ToList();

            ViewBag.Search = search;
            return View(lessons);
        }

        // GET: SwimmingLesson/Create
        public ActionResult Create()
        {
            ViewBag.PoolID = new SelectList(db.Pools, "PoolID", "Name");
            return View();
        }

        // POST: SwimmingLesson/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SwimmingLesson swimmingLesson)
        {
            if (ModelState.IsValid)
            {
                db.SwimmingLessons.Add(swimmingLesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PoolID = new SelectList(db.Pools, "PoolID", "Name", swimmingLesson.PoolID);
            return View(swimmingLesson);
        }

        // GET: SwimmingLesson/Edit/5
        public ActionResult Edit(int id)
        {
            var swimmingLesson = db.SwimmingLessons.Find(id);
            if (swimmingLesson == null)
            {
                return HttpNotFound();
            }

            ViewBag.PoolID = new SelectList(db.Pools, "PoolID", "Name", swimmingLesson.PoolID);
            return View(swimmingLesson);
        }

        // POST: SwimmingLesson/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SwimmingLesson swimmingLesson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(swimmingLesson).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PoolID = new SelectList(db.Pools, "PoolID", "Name", swimmingLesson.PoolID);
            return View(swimmingLesson);
        }

        // GET: SwimmingLesson/Delete/5
        public ActionResult Delete(int id)
        {
            var swimmingLesson = db.SwimmingLessons.Find(id);
            if (swimmingLesson == null)
            {
                return HttpNotFound();
            }

            return View(swimmingLesson);
        }

        // POST: SwimmingLesson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var swimmingLesson = db.SwimmingLessons.Find(id);
            db.SwimmingLessons.Remove(swimmingLesson);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
