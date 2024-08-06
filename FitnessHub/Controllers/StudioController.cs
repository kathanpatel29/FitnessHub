using System.Linq;
using System.Web.Mvc;
using FitnessHub.Models;

namespace FitnessHub.Controllers
{
    public class StudioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: studio
        public ActionResult Index()
        {
            var studios = db.Studios.ToList();
            return View(studios);
        }

        // GET: studio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: studio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Studio studio)
        {
            if (ModelState.IsValid)
            {
                db.Studios.Add(studio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studio);
        }

        // GET: studio/Edit/5
        public ActionResult Edit(int id)
        {
            var studio = db.Studios.Find(id);
            if (studio == null)
            {
                return HttpNotFound();
            }

            return View(studio);
        }

        // POST: studio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Studio studio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studio).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studio);
        }

        // GET: studio/Delete/5
        public ActionResult Delete(int id)
        {
            var studio = db.Studios.Find(id);
            if (studio == null)
            {
                return HttpNotFound();
            }

            return View(studio);
        }

        // POST: studio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var studio = db.Studios.Find(id);
            db.Studios.Remove(studio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
