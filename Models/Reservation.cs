using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenceVoyage.Models
{
    public class Reservation
    {
        [Key]
        public int Id_reservation { get; set; }
        
        [ForeignKey("Client")] // Clé étrangère pour la propriété Id_client
        public int Id_client { get; set; }
        public  Client Client { get; set; } // Navigation property pour la relation avec Client
        [Required]
        public DateTime DateDebut { get; set; }
        [Required]
        public DateTime DateFin {  get; set; }
        
        [ForeignKey("Chambre")] // Clé étrangère pour la propriété Id_client
        public int Id_chambre { get; set; }
        public Chambre Chambre { get; set; }


    }
}
