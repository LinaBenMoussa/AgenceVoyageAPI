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
        public string? Name { get; set; }
        public string? Prenom { get; set; }
        public int? Telephone { get; set; }
        public DateTime DateNaissance { get; set; }
        
        [ForeignKey("Compte")]
        public int Id_compte { get; set; }
       


    }

}
