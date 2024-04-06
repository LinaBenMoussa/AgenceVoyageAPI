using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenceVoyage.Models
{
    public class Chambre
    {
        [Key]
        public int Id_chambre { get; set; }
        
        public int nom { get; set; }

        [Required]
        public int Nbre_personnes { get; set; }
        
        [ForeignKey("Hotel")]
        public int Id_hotel { get; set; }
        public Hotel Hotel { get; set; } // Navigation property pour la relation avec Hotel
        [Required]
        public float Prix { get; set; }
    }
}
