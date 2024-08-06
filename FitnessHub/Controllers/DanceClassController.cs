using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FitnessHub.Models;

namespace FitnessHub.Controllers
{
    public class DanceClassController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DanceClass
        public ActionResult Index(string search = "")
        {
            var classes = db.DanceClasses
                .Where(c => c.Name.Contains(search) || c.Instructor.Contains(search))
                .Select(c => new DanceClassDto
                {
                    ClassID = c.ClassID,
                    StudioID = c.StudioID,
                    Name = c.Name,
                    Instructor = c.Instructor,
                    Schedule = c.Schedule,
                    Duration = c.Duration,
                    Price = c.Price,
                    Status = c.Status
                })
                .ToList();

            ViewBag.Search = search;
            return View(classes);
        }

        // GET: DanceClass/Create
        public ActionResult Create()
        {
            ViewBag.StudioID = new SelectList(db.Studios, "StudioID", "Name");
            return View();
        }

        // POST: DanceClass/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DanceClassDto danceClassDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var danceClass = new DanceClass
                    {
                        StudioID = danceClassDto.StudioID,
                        Name = danceClassDto.Name,
                        Instructor = danceClassDto.Instructor,
                        Schedule = danceClassDto.Schedule,
                        Duration = danceClassDto.Duration,
                        Price = danceClassDto.Price,
                        Status = danceClassDto.Status
                    };

                    db.DanceClasses.Add(danceClass);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception and show an error message
                    ViewBag.ErrorMessage = "An error occurred while creating the dance class.";
                }
            }

            ViewBag.StudioID = new SelectList(db.Studios, "StudioID", "Name", danceClassDto.StudioID);
            return View(danceClassDto);
        }

        // GET: DanceClass/Edit/5
        public ActionResult Edit(int id)
        {
            var danceClass = db.DanceClasses
                .Where(c => c.ClassID == id)
                .Select(c => new DanceClassDto
                {
                    ClassID = c.ClassID,
                    StudioID = c.StudioID,
                    Name = c.Name,
                    Instructor = c.Instructor,
                    Schedule = c.Schedule,
                    Duration = c.Duration,
                    Price = c.Price,
                    Status = c.Status
                })
                .FirstOrDefault();

            if (danceClass == null)
            {
                return HttpNotFound();
            }

            ViewBag.StudioID = new SelectList(db.Studios, "StudioID", "Name", danceClass.StudioID);
            return View(danceClass);
        }

        // POST: DanceClass/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DanceClassDto danceClassDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var danceClass = await db.DanceClasses.FindAsync(danceClassDto.ClassID);

                    if (danceClass == null)
                    {
                        return HttpNotFound();
                    }

                    // Update properties
                    danceClass.StudioID = danceClassDto.StudioID;
                    danceClass.Name = danceClassDto.Name;
                    danceClass.Instructor = danceClassDto.Instructor;
                    danceClass.Schedule = danceClassDto.Schedule;
                    danceClass.Duration = danceClassDto.Duration;
                    danceClass.Price = danceClassDto.Price;
                    danceClass.Status = danceClassDto.Status;

                    db.Entry(danceClass).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception and show an error message
                    ViewBag.ErrorMessage = "An error occurred while updating the dance class.";
                }
            }

            ViewBag.StudioID = new SelectList(db.Studios, "StudioID", "Name", danceClassDto.StudioID);
            return View(danceClassDto);
        }

        // GET: DanceClass/Delete/5
        public ActionResult Delete(int id)
        {
            var danceClass = db.DanceClasses
                .Where(c => c.ClassID == id)
                .Select(c => new DanceClassDto
                {
                    ClassID = c.ClassID,
                    StudioID = c.StudioID,
                    Name = c.Name,
                    Instructor = c.Instructor,
                    Schedule = c.Schedule,
                    Duration = c.Duration,
                    Price = c.Price,
                    Status = c.Status
                })
                .FirstOrDefault();

            if (danceClass == null)
            {
                return HttpNotFound();
            }

            return View(danceClass);
        }

        // POST: DanceClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var danceClass = await db.DanceClasses.FindAsync(id);

                if (danceClass == null)
                {
                    return HttpNotFound();
                }

                db.DanceClasses.Remove(danceClass);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception and show an error message
                ViewBag.ErrorMessage = "An error occurred while deleting the dance class.";
            }

            return RedirectToAction("Index");
        }
    }
}
