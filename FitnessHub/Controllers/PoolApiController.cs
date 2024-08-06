using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FitnessHub.Models;

namespace FitnessHub.Controllers.Api
{
    public class PoolApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<PoolDto> GetPools()
        {
            return db.Pools
                .Select(p => new PoolDto
                {
                    PoolID = p.PoolID,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                }).ToList();
        }

        [HttpGet]
        public IHttpActionResult GetPool(int id)
        {
            var pool = db.Pools
                .Select(p => new PoolDto
                {
                    PoolID = p.PoolID,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                }).FirstOrDefault(p => p.PoolID == id);

            if (pool == null)
            {
                return NotFound();
            }

            return Ok(pool);
        }

        [HttpPost]
        public IHttpActionResult CreatePool(PoolDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pool = new Pool
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl
            };

            db.Pools.Add(pool);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pool.PoolID }, dto);
        }

        [HttpPut]
        public IHttpActionResult UpdatePool(int id, PoolDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pool = db.Pools.Find(id);
            if (pool == null)
            {
                return NotFound();
            }

            pool.Name = dto.Name;
            pool.Description = dto.Description;
            pool.ImageUrl = dto.ImageUrl;

            db.SaveChanges();

            return Ok(dto);
        }

        [HttpDelete]
        public IHttpActionResult DeletePool(int id)
        {
            var pool = db.Pools.Find(id);
            if (pool == null)
            {
                return NotFound();
            }

            db.Pools.Remove(pool);
            db.SaveChanges();

            return Ok();
        }
    }
}
