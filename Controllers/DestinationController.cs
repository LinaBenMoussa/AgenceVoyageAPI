using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgenceVoyage.Models;

namespace AgenceVoyage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly ClientDbContext _context;

        public DestinationsController(ClientDbContext context)
        {
            _context = context;
        }

        // GET: api/Destinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination>>> GetDestinations()
        {
            return await _context.Destination.ToListAsync();
        }

        // GET: api/Destinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestination(int id)
        {
            var destination = await _context.Destination.FindAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            return destination;
        }

        // POST: api/Destinations
        [HttpPost]
        public async Task<ActionResult<Destination>> PostDestination(Destination destination)
        {
            _context.Destination.Add(destination);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDestination), new { id = destination.Id_destination }, destination);
        }

        // PUT: api/Destinations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestination(int id, Destination destination)
        {
            if (id != destination.Id_destination)
            {
                return BadRequest();
            }

            _context.Entry(destination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
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

        // DELETE: api/Destinations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var destination = await _context.Destination.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }

            _context.Destination.Remove(destination);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Destinations/total
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalDestinations()
        {
            var totalDestinations = await _context.Destination.CountAsync();
            return totalDestinations;
        }

        private bool DestinationExists(int id)
        {
            return _context.Destination.Any(e => e.Id_destination == id);
        }

        [HttpGet("SearchByNom")]
        public async Task<ActionResult<IEnumerable<Destination>>> SearchByNom(string nom)
        {
            var destinations = await _context.Destination
                .Where(d => d.nom.ToLower().Contains(nom.ToLower()))
                .ToListAsync();

            if (destinations == null || destinations.Count == 0)
            {
                return NotFound();
            }

            return destinations;
        }
    }
}
