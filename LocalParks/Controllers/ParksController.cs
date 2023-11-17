using LocalParks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LocalParks.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ParksController : ControllerBase
    {
        private readonly LocalParksContext _db;

        public ParksController(LocalParksContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Park>>> Get(
            string name,
            string location,
            string summary
        )
        {
            IQueryable<Park> query = _db.Parks.AsQueryable();

            if (name != null)
            {
                query = query.Where(entry => entry.Name == name);
            }

            if (location != null)
            {
                query = query.Where(entry => entry.Location == location);
            }

            if (summary != null)
            {
                query = query.Where(entry => entry.Summary == summary);
            }

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Park>> GetPark(int id)
        {
            Park park = await _db.Parks.FindAsync(id);

            if (park == null)
            {
                return NotFound();
            }

            return park;
        }

        [HttpPost]
        public async Task<ActionResult<Park>> Post(Park park)
        {
            _db.Parks.Add(park);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPark), new { id = park.ParkId }, park);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPark(int id, Park park)
        {
            if (id != park.ParkId)
            {
                return BadRequest();
            }

            _db.Parks.Update(park);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePark(int id)
        {
            var park = await _db.Parks.FindAsync(id);
            if (park == null)
            {
                return NotFound();
            }

            _db.Parks.Remove(park);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkExists(int id)
        {
            return _db.Parks.Any(e => e.ParkId == id);
        }
    }
}
