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
    public class HotelsController : ControllerBase
    {
        private readonly ClientDbContext _context;

        public HotelsController(ClientDbContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            return await _context.Hotels.ToListAsync();
        }
        
        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // POST: api/Hotels/search
        [HttpPost("search")]
        public async Task<ActionResult<List<Hotel>>> PostSearchHotels(FilterSearch filter)
        {
            if (filter.Id_destination != 0)
            {
                if (filter.MinPrice != 0 || filter.MaxPrice != 0)
                {
                    return await _context.Hotels
                .Where(h => h.Id_destination == filter.Id_destination & h.Prix >= filter.MinPrice & (h.Prix <= filter.MaxPrice || filter.MaxPrice==0))
                .ToListAsync();
                }
                else
                {
                    //l'utilisateur a entré seulement la destination
                    return await _context.Hotels
                .Where(h => h.Id_destination == filter.Id_destination)
                .ToListAsync();
                }
            }
            else if(filter.Id_destination == 0 && (filter.MinPrice !=0 || filter.MaxPrice != 0))
            {
                //l'utilisateur a entré seulement le prix
                return await _context.Hotels
           .Where(h => h.Prix >= filter.MinPrice & (h.Prix <= filter.MaxPrice || filter.MaxPrice == 0))
           .ToListAsync();
            }
            else
            {
                return await _context.Hotels.ToListAsync();
            }
           

        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id_hotel)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.Id_hotel }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id_hotel == id);
        }
    }
}
