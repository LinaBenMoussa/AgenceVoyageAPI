using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgenceVoyage.Models;
using AgenceVoyage.DtoModels;

namespace AgenceVoyage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ClientDbContext _context;

        public ReservationsController(ClientDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.Include(r => r.Client).ToListAsync();
        }
        [HttpGet("liste")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GettReservations()
        {
            return await _context.Reservations
                                 .Include(r => r.Client)
                                 .Include(r => r.Chambre)
                                  .ThenInclude(c => c.Hotel)
                                 .ToListAsync();
        }
        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id_reservation)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> PostReservation(ReservationDto reservationDto)
        {
            var reservation = new Reservation
            {
                Id_client = reservationDto.Id_client,
                DateDebut = reservationDto.DateDebut,
                DateFin = reservationDto.DateFin,
                Id_chambre = reservationDto.Id_chambre
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id_reservation }, reservation);
        }
        [HttpGet("client/{userId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByUserId(int userId)
        {
            var reservations = await _context.Reservations
                                             .Where(r => r.Id_client == userId)
                                             .ToListAsync();

            if (reservations == null)
            {
                return NotFound();
            }

            return reservations;
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Reservations/total
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalReservations()
        {
            var totalReservations = await _context.Reservations.CountAsync();
            return totalReservations;
        }

        // GET: api/Reservations/hotels
        [HttpGet("hotels")]
        public async Task<ActionResult<IEnumerable<object>>> GetReservationsByHotel()
        {
            var reservations = await _context.Reservations
                                             .GroupBy(r => r.Chambre.Id_hotel)
                                             .Select(g => new {
                                                 HotelId = g.Key,
                                                 TotalReservations = g.Count()
                                             })
                                                           .ToListAsync();

            var hotelsWithReservations = new List<object>();

            foreach (var item in reservations)
            {
                var hotel = await _context.Hotels.FindAsync(item.HotelId);
                hotelsWithReservations.Add(new
                {
                    Nom = hotel?.nom ?? "Unknown", // Replace with actual property name
                    TotalReservations = item.TotalReservations
                });
            }

            return hotelsWithReservations;
        }

        // GET: api/Reservations/hotel/5
        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationsByHotel(int hotelId)
        {
            var reservations = await _context.Reservations
                                             .Where(r => r.Chambre.Id_hotel == hotelId)
                                             .Select(r => new ReservationDto
                                             {
                                                 Id_client = r.Id_client,
                                                 DateDebut = r.DateDebut,
                                                 DateFin = r.DateFin,
                                                 Id_chambre = r.Id_chambre
                                             })
                                             .ToListAsync();

            if (reservations == null)
            {
                return NotFound();
            }

            return reservations;
        }
        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id_reservation == id);
        }
    }
}