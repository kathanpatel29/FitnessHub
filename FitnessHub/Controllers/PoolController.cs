using System.Linq;
using System.Web.Mvc;
using FitnessHub.Models;

namespace FitnessHub.Controllers
{
    public class PoolController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pool
        public ActionResult Index()
        {
            var pools = db.Pools.ToList();
            return View(pools);
        }

        // GET: Pool/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pool/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pool pool)
        {
            if (ModelState.IsValid)
            {
                db.Pools.Add(pool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pool);
        }

        // GET: Pool/Edit/5
        public ActionResult Edit(int id)
        {
            var pool = db.Pools.Find(id);
            if (pool == null)
            {
                return HttpNotFound();
            }

            return View(pool);
        }

        // POST: Pool/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pool pool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pool).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pool);
        }

        // GET: Pool/Delete/5
        public ActionResult Delete(int id)
        {
            var pool = db.Pools.Find(id);
            if (pool == null)
            {
                return HttpNotFound();
            }

            return View(pool);
        }

        // POST: Pool/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pool = db.Pools.Find(id);
            db.Pools.Remove(pool);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
