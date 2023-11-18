using LocalParks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using LocalParks.Contracts;
using Newtonsoft.Json;

namespace LocalParks.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ParksController : ControllerBase
    {
        private readonly LocalParksContext _db;
        private readonly IParkRepository _repository;

        public ParksController(LocalParksContext db, IParkRepository repository)
        {
            _db = db;
            _repository = repository;
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

        [HttpGet]
        [Route("paging-filter")]
        public IActionResult GetParkPagingData([FromQuery] PagedParameters parkParameters)
        {
            var park = _repository.GetParks(parkParameters);

            var metadata = new
            {
                park.TotalCount,
                park.PageSize,
                park.CurrentPage,
                park.TotalPages,
                park.HasNext,
                park.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(park);
        }

        [HttpGet]
        [Route("getpaging-by-param")]
        public async Task<ActionResult<IEnumerable<Park>>> GetparksByFilter(
            PagedParameters ownerParameters
        )
        {
            if (_db.Parks == null)
            {
                return NotFound();
            }
            return await _db.Parks
                .OrderBy(on => on.ParkId)
                .Skip((ownerParameters.PageNumber - 1) * ownerParameters.PageSize)
                .Take(ownerParameters.PageSize)
                .ToListAsync();
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
