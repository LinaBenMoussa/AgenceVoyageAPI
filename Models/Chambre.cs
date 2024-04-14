using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenceVoyage.Models
{
    public class Chambre
    {
        [Key]
        public int Id_chambre { get; set; }
        public string img { get; set; }

        public string nom { get; set; }
        public string surface { get; set; }
        [Required]
        public int Nbre_personnes { get; set; }

        [ForeignKey("Hotel")]
        public int Id_hotel { get; set; }
        [Required]
        public float Prix { get; set; }

    }
}
