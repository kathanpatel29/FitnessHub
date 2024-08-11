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
    public class PoolApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly string imagesFolder = HttpContext.Current.Server.MapPath("~/Images/Pools/");

        // GET: api/Pool
        public IHttpActionResult GetPools()
        {
            var pools = db.Pools.ToList();
            return Ok(pools);
        }

        // GET: api/Pool/5
        public IHttpActionResult GetPool(int id)
        {
            var pool = db.Pools.Find(id);
            if (pool == null)
            {
                return NotFound();
            }
            return Ok(pool);
        }

        // POST: api/Pool
        [HttpPost]
        public IHttpActionResult CreatePool([FromBody] PoolDto poolDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pool = new Pool
            {
                Name = poolDto.Name,
                Location = poolDto.Location,
                Description = poolDto.Description
            };

            if (!string.IsNullOrEmpty(poolDto.ImageUrl))
            {
                pool.ImageUrl = poolDto.ImageUrl;
            }
            else
            {
                pool.ImageUrl = "/Images/Pools/default.png"; // Default image if none provided
            }

            db.Pools.Add(pool);
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + pool.PoolID), pool);
        }

        // PUT: api/Pool/5
        [HttpPut]
        public IHttpActionResult UpdatePool(int id, [FromBody] PoolDto poolDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var poolInDb = db.Pools.Find(id);
            if (poolInDb == null)
            {
                return NotFound();
            }

            poolInDb.Name = poolDto.Name;
            poolInDb.Location = poolDto.Location;
            poolInDb.Description = poolDto.Description;

            if (!string.IsNullOrEmpty(poolDto.ImageUrl))
            {
                poolInDb.ImageUrl = poolDto.ImageUrl;
            }

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Pool/5
        [HttpDelete]
        public IHttpActionResult DeletePool(int id)
        {
            var pool = db.Pools.Find(id);
            if (pool == null)
            {
                return NotFound();
            }

            // Delete the image file if exists
            var imagePath = HttpContext.Current.Server.MapPath(pool.ImageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            db.Pools.Remove(pool);
            db.SaveChanges();

            return Ok();
        }
    }
}
