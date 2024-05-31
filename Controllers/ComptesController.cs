using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgenceVoyage.Models;

namespace AgenceVoyage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComptesController : ControllerBase
    {
        private readonly ClientDbContext _context;

        public ComptesController(ClientDbContext context)
        {
            _context = context;
        }

        // GET: api/Comptes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compte>>> GetComptes()
        {
            return await _context.Comptes.ToListAsync();
        }

        // GET: api/Comptes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compte>> GetCompte(int id)
        {
            var compte = await _context.Comptes.FindAsync(id);

            if (compte == null)
            {
                return NotFound();
            }

            return compte;
        }

        // PUT: api/Comptes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompte(int id, Compte compte)
        {
            if (id != compte.Id_compte)
            {
                return BadRequest();
            }

            _context.Entry(compte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompteExists(id))
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

        // POST: api/Comptes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compte>> PostCompte(Compte compte)
        {
            _context.Comptes.Add(compte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompte", new { id = compte.Id_compte }, compte);
        }

        // DELETE: api/Comptes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompte(int id)
        {
            var compte = await _context.Comptes.FindAsync(id);
            if (compte == null)
            {
                return NotFound();
            }

            _context.Comptes.Remove(compte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompteExists(int id)
        {
            return _context.Comptes.Any(e => e.Id_compte == id);
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Compte loginModel)
        {
            var user = _context.Comptes.SingleOrDefault(x => x.Email == loginModel.Email && x.MotDePasse == loginModel.MotDePasse);

            if (user == null)
            {
                return BadRequest(new { message = "Email ou mot de passe incorrect." });
            }

            // Générez votre token ici (utilisez JWT ou tout autre mécanisme d'authentification)
            //string token = generatetoken(user.id_compte);

            return Ok(user);
        }
        [HttpPost("registerCompte")]
        public IActionResult RegisterCompte([FromBody] Compte registerModel)
        {
            // Vérifiez si l'email est déjà utilisé
            if (_context.Comptes.Any(x => x.Email == registerModel.Email))
            {
                return BadRequest(new { message = "Cet email est déjà utilisé." });
            }

            // Créez un nouveau compte
            var compte = new Compte
            {
                Email = registerModel.Email,
                MotDePasse = registerModel.MotDePasse,
                role = registerModel.role,
                Token = registerModel.Token,
            };

            _context.Comptes.Add(compte);
            _context.SaveChanges();

            // Retournez le compte créé dans la réponse
            return Ok(new { message = "Inscription réussie.", id_compte = compte.Id_compte });
        }


        [HttpPost("registerClient")]
        public IActionResult RegisterClient([FromBody] Client clientModel)
        {
            
            
            var client = new Client
            {
                Id_compte = clientModel.Id_compte,
                Name = clientModel.Name,
                Prenom = clientModel.Prenom,
                Telephone = clientModel.Telephone,
                DateNaissance = clientModel.DateNaissance
            };
            _context.Clients.Add(client);
            _context.SaveChanges();

            return Ok(new { message = "Inscription réussie." });
        }


    }
}
