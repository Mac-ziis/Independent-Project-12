using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowellApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Authorization;

namespace LocalParks.Controllers
{
    [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ParksController : ControllerBase
  {
    private readonly LocalParksContext _db;
    public BooksController(LocalParksContext db)
    {
      _db = db;
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Park>>> Get(string Name, string Location,  string summary)
    {
        IQueryable<Park> query = _db.Parks.AsQueryable();

        if (Name != null)
        {
            query = query.Where(entry => entry.Name == name);
        }

        if (Location != null)
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
        return CreatedAtAction(nameof(GetPark), new{ id = park.ParkId }, park);
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