using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgenceVoyage.Models
{
    public class Photo
    {
        [Key]
        public int Id_photo { get; set; }
        [Required]
        public string? Url { get; set; }
        public int Id_hotel { get; set; }
    }
}
