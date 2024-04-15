using AgenceVoyage.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgenceVoyage.DtoModels
{
    public class ReservationDto
    {
       
        

        public int Id_client { get; set; }
        
        
        public DateTime DateDebut { get; set; }
        
        public DateTime DateFin { get; set; }

         // Clé étrangère pour la propriété Id_client
        public int Id_chambre { get; set; }
        
    }
}
