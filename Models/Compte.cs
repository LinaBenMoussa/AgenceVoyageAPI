using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenceVoyage.Models
{
    public class Compte
    {
        [Key]
        public int Id_compte { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? MotDePasse { get; set; }
        public int role { get; set; }
        [Required]
        public string Token { get; set; }

    }

}
