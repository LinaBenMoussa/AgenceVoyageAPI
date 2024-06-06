using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgenceVoyage.Models;
using AgenceVoyage.DtoModels;
using System.Net.Sockets;

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
            return await _context.Reservations.Include(r=>r.Client).ToListAsync();
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
            
            var reservation = new Reservation(){
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
        [HttpGet("check-availability")]
        public async Task<ActionResult<bool>> CheckAvailability(int chambreId, DateTime startDate, DateTime endDate)
        {
            var overlappingReservations = await _context.Reservations
                .Where(r => r.Id_chambre == chambreId
                &&
                            //((r.DateDebut>=startDate && r.DateDebut<=endDate) || (r.DateFin<=endDate && r.DateFin>=endDate))

                            ((r.DateDebut <= startDate && r.DateFin >= startDate) ||
                             (r.DateDebut <= endDate && r.DateFin >= endDate) ||
                             (r.DateDebut >= startDate && r.DateFin <= endDate)))
                .ToListAsync();

            if (overlappingReservations.Any())
            {
                return Ok(false); // La chambre est déjà réservée pour cette période
            }

            return Ok(true); // La chambre est disponible pour cette période
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

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id_reservation == id);
        }
    }
}
