using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessHub.Models;

namespace FitnessHub.Controllers
{
    public class PoolController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly string imagesFolder = "~/Images/Pools/";

        // GET: Pool
        public ActionResult Index()
        {
            var pools = db.Pools.ToList();
            return View(pools);
        }

        // GET: Pool/Details/5
        public ActionResult Details(int id)
        {
            var pool = db.Pools.Find(id);
            if (pool == null)
            {
                return HttpNotFound();
            }
            return View(pool);
        }

        // GET: Pool/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pool/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pool pool, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath(imagesFolder), fileName);
                    imageFile.SaveAs(filePath);
                    pool.ImageUrl = Path.Combine(imagesFolder, fileName);
                }
                else
                {
                    pool.ImageUrl = "/Images/Pools/default.png"; // Default image if none provided
                }

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
        public ActionResult Edit(Pool pool, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath(imagesFolder), fileName);
                    imageFile.SaveAs(filePath);
                    pool.ImageUrl = Path.Combine(imagesFolder, fileName);
                }

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
            if (pool != null)
            {
                // Delete the image file if exists
                var imagePath = Server.MapPath(pool.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                db.Pools.Remove(pool);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
