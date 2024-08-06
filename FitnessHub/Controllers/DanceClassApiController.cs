using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FitnessHub.Models;

namespace FitnessHub.Controllers.Api
{
    public class DanceClassApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<DanceClassDto> GetDanceClasses(string search = "")
        {
            return db.DanceClasses
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
                }).ToList();
        }

        [HttpGet]
        public IHttpActionResult GetDanceClass(int id)
        {
            var danceClass = db.DanceClasses
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
                }).FirstOrDefault(c => c.ClassID == id);

            if (danceClass == null)
            {
                return NotFound();
            }

            return Ok(danceClass);
        }

        [HttpPost]
        public IHttpActionResult CreateDanceClass(DanceClassDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var danceClass = new DanceClass
            {
                StudioID = dto.StudioID,
                Name = dto.Name,
                Instructor = dto.Instructor,
                Schedule = dto.Schedule,
                Duration = dto.Duration,
                Price = dto.Price,
                Status = dto.Status
            };

            db.DanceClasses.Add(danceClass);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = danceClass.ClassID }, dto);
        }

        [HttpPut]
        public IHttpActionResult UpdateDanceClass(int id, DanceClassDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var danceClass = db.DanceClasses.Find(id);
            if (danceClass == null)
            {
                return NotFound();
            }

            danceClass.StudioID = dto.StudioID;
            danceClass.Name = dto.Name;
            danceClass.Instructor = dto.Instructor;
            danceClass.Schedule = dto.Schedule;
            danceClass.Duration = dto.Duration;
            danceClass.Price = dto.Price;
            danceClass.Status = dto.Status;

            db.SaveChanges();

            return Ok(dto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteDanceClass(int id)
        {
            var danceClass = db.DanceClasses.Find(id);
            if (danceClass == null)
            {
                return NotFound();
            }

            db.DanceClasses.Remove(danceClass);
            db.SaveChanges();

            return Ok();
        }
    }
}
