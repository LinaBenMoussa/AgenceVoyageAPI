using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenceVoyage.Models
{
    public class Hotel
    {
        [Key]
        public int Id_hotel { get; set; }
        [Required]
        public String nom { get; set; }
        [Required]
        public int Categorie {  get; set; }
        [Required]
        public int Nbre_chambres { get; set; }
        [Required]
        public String? Localisation { get; set; }
        [ForeignKey("Destination")]
        public int Id_destination { get; set; }
        public ICollection<Photo> Photos { get; set; } = new List<Photo>(); 
        public ICollection<Chambre> Chambres { get; set; } = new List<Chambre>();
    }
}
