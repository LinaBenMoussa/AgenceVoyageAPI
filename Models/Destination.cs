using System.ComponentModel.DataAnnotations;

namespace AgenceVoyage.Models
{
    public class Destination
    {
        [Key]
        public int Id_destination { get; set; }
        [Required]
        public string? nom { get; set; }

    }
}
