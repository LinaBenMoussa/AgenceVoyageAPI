using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgenceVoyage.Models;
using AgenceVoyage.DtoModels;

namespace AgenceVoyage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChambresController : ControllerBase
    {
        private readonly ClientDbContext _context;

        public ChambresController(ClientDbContext context)
        {
            _context = context;
        }

        // GET: api/Chambres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chambre>>> GetChambres()
        {
            return await _context.Chambres.ToListAsync();
        }

        // GET: api/Chambres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chambre>> GetChambre(int id)
        {
            var chambre = await _context.Chambres.FindAsync(id);

            if (chambre == null)
            {
                return NotFound();
            }

            return chambre;
        }

        // GET: api/Chambres/ByHotel/5
        [HttpGet("ByHotel/{idHotel}")]
        public async Task<ActionResult<IEnumerable<Chambre>>> GetChambresByIdHotel(int idHotel)
        {
            var chambres = await _context.Chambres
                .Where(c => c.Id_hotel == idHotel)
                .ToListAsync();

            if (chambres == null || chambres.Count == 0)
            {
                return NotFound();
            }

            return chambres;
        }

        // PUT: api/Chambres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChambre(int id, Chambre chambre)
        {
            if (id != chambre.Id_chambre)
            {
                return BadRequest();
            }

            _context.Entry(chambre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChambreExists(id))
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

        // POST: api/Chambres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chambre>> PostChambre(Chambre chambre)
        {
            _context.Chambres.Add(chambre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChambre", new { id = chambre.Id_chambre }, chambre);
        }

        // DELETE: api/Chambres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChambre(int id)
        {
            var chambre = await _context.Chambres.FindAsync(id);
            if (chambre == null)
            {
                return NotFound();
            }

            _context.Chambres.Remove(chambre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Chambres/total
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalChambres()
        {
            var totalChambres = await _context.Chambres.CountAsync();
            return totalChambres;
        }
        [HttpGet("rooms-per-hotel")]
        public async Task<ActionResult<IEnumerable<RoomsPerHotelDto>>> GetRoomsPerHotel()
        {
            var roomsPerHotel = await _context.Hotels
                .Select(h => new RoomsPerHotelDto
                {
                    HotelName = h.nom,
                    RoomCount = h.Chambres.Count()
                })
                .ToListAsync();

            return Ok(roomsPerHotel);
        }

        private bool ChambreExists(int id)
        {
            return _context.Chambres.Any(e => e.Id_chambre == id);
        }
    }
}
