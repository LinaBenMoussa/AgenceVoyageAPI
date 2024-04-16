using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenceVoyage.Models
{
    public class Compte
    {
        [Key]
        public int Id_compte { get; set; }
        
        public string? Email { get; set; }
        
        public string? MotDePasse { get; set; }
        public int role { get; set; }
       
        public string Token { get; set; }

    }

}
