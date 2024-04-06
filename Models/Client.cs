using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace AgenceVoyage.Models
{
    public class Client
    {
        [Key]
        public int Id_client { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Prenom { get; set; }
        [Required]
        public int? Telephone { get; set; }
        [Required]
        public DateTime DateNaissance { get; set; }
        
        [ForeignKey("Compte")]
        public int Id_compte { get; set; }
        public Compte Compte { get; set; }
        //public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }

}
