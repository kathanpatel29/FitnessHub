using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FitnessHub.Models;

namespace FitnessHub.Controllers.Api
{
    public class studioApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<StudioDto> Getstudios()
        {
            return db.Studios
                .Select(p => new StudioDto
                {
                    StudioID = p.StudioID,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                }).ToList();
        }

        [HttpGet]
        public IHttpActionResult Getstudio(int id)
        {
            var studio = db.Studios
                .Select(p => new StudioDto
                {
                    StudioID = p.StudioID,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                }).FirstOrDefault(p => p.StudioID == id);

            if (studio == null)
            {
                return NotFound();
            }

            return Ok(studio);
        }

        [HttpPost]
        public IHttpActionResult Createstudio(StudioDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studio = new Studio
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl
            };

            db.Studios.Add(studio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = studio.StudioID }, dto);
        }

        [HttpPut]
        public IHttpActionResult Updatestudio(int id, StudioDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studio = db.Studios.Find(id);
            if (studio == null)
            {
                return NotFound();
            }

            studio.Name = dto.Name;
            studio.Description = dto.Description;
            studio.ImageUrl = dto.ImageUrl;

            db.SaveChanges();

            return Ok(dto);
        }

        [HttpDelete]
        public IHttpActionResult Deletestudio(int id)
        {
            var studio = db.Studios.Find(id);
            if (studio == null)
            {
                return NotFound();
            }

            db.Studios.Remove(studio);
            db.SaveChanges();

            return Ok();
        }
    }
}
