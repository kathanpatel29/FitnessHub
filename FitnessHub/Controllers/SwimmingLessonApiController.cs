using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FitnessHub.Models;

namespace FitnessHub.Controllers.Api
{
    public class SwimmingLessonApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<SwimmingLessonDto> GetSwimmingLessons(string search = "")
        {
            return db.SwimmingLessons
                .Where(l => l.Name.Contains(search) || l.Instructor.Contains(search))
                .Select(l => new SwimmingLessonDto
                {
                    LessonID = l.LessonID,
                    PoolID = l.PoolID,
                    Name = l.Name,
                    Instructor = l.Instructor,
                    Schedule = l.Schedule,
                    Duration = l.Duration,
                    Price = l.Price,
                    Status = l.Status
                }).ToList();
        }

        [HttpGet]
        public IHttpActionResult GetSwimmingLesson(int id)
        {
            var lesson = db.SwimmingLessons
                .Select(l => new SwimmingLessonDto
                {
                    LessonID = l.LessonID,
                    PoolID = l.PoolID,
                    Name = l.Name,
                    Instructor = l.Instructor,
                    Schedule = l.Schedule,
                    Duration = l.Duration,
                    Price = l.Price,
                    Status = l.Status
                }).FirstOrDefault(l => l.LessonID == id);

            if (lesson == null)
            {
                return NotFound();
            }

            return Ok(lesson);
        }

        [HttpPost]
        public IHttpActionResult CreateSwimmingLesson(SwimmingLessonDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lesson = new SwimmingLesson
            {
                PoolID = dto.PoolID,
                Name = dto.Name,
                Instructor = dto.Instructor,
                Schedule = dto.Schedule,
                Duration = dto.Duration,
                Price = dto.Price,
                Status = dto.Status
            };

            db.SwimmingLessons.Add(lesson);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lesson.LessonID }, dto);
        }

        [HttpPut]
        public IHttpActionResult UpdateSwimmingLesson(int id, SwimmingLessonDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lesson = db.SwimmingLessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            lesson.PoolID = dto.PoolID;
            lesson.Name = dto.Name;
            lesson.Instructor = dto.Instructor;
            lesson.Schedule = dto.Schedule;
            lesson.Duration = dto.Duration;
            lesson.Price = dto.Price;
            lesson.Status = dto.Status;

            db.SaveChanges();

            return Ok(dto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteSwimmingLesson(int id)
        {
            var lesson = db.SwimmingLessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            db.SwimmingLessons.Remove(lesson);
            db.SaveChanges();

            return Ok();
        }
    }
}
