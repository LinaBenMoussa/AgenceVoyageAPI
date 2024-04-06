using Microsoft.EntityFrameworkCore;

namespace AgenceVoyage.Models
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options) { }

        public DbSet<Client> Clients {  get; set; }
        public DbSet<Chambre> Chambres { get; set; }
        public DbSet<Destination> Destination { get; set; }

        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
