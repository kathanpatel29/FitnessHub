using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FitnessHub.Models;

namespace FitnessHub.Controllers.Api
{
    public class StudioApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly string imagesFolder = HttpContext.Current.Server.MapPath("~/Images/Studios/");

        // GET: api/Studio
        public IHttpActionResult GetStudios()
        {
            var studios = db.Studios.ToList();
            return Ok(studios);
        }

        // GET: api/Studio/5
        public IHttpActionResult GetStudio(int id)
        {
            var studio = db.Studios.Find(id);
            if (studio == null)
            {
                return NotFound();
            }
            return Ok(studio);
        }

        // POST: api/Studio
        [HttpPost]
        public IHttpActionResult CreateStudio([FromBody] StudioDto studioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studio = new Studio
            {
                Name = studioDto.Name,
                Location = studioDto.Location,
                Description = studioDto.Description
            };

            if (!string.IsNullOrEmpty(studioDto.ImageUrl))
            {
                studio.ImageUrl = studioDto.ImageUrl;
            }
            else
            {
                studio.ImageUrl = "/Images/Studios/default.png"; // Default image if none provided
            }

            db.Studios.Add(studio);
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + studio.StudioID), studio);
        }

        // PUT: api/Studio/5
        [HttpPut]
        public IHttpActionResult UpdateStudio(int id, [FromBody] StudioDto studioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studioInDb = db.Studios.Find(id);
            if (studioInDb == null)
            {
                return NotFound();
            }

            studioInDb.Name = studioDto.Name;
            studioInDb.Location = studioDto.Location;
            studioInDb.Description = studioDto.Description;

            if (!string.IsNullOrEmpty(studioDto.ImageUrl))
            {
                studioInDb.ImageUrl = studioDto.ImageUrl;
            }

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Studio/5
        [HttpDelete]
        public IHttpActionResult DeleteStudio(int id)
        {
            var studio = db.Studios.Find(id);
            if (studio == null)
            {
                return NotFound();
            }

            // Delete the image file if exists
            var imagePath = HttpContext.Current.Server.MapPath(studio.ImageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            db.Studios.Remove(studio);
            db.SaveChanges();

            return Ok();
        }
    }
}
