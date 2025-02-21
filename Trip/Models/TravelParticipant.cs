using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trip.Models.Extra;

namespace Trip.Models
{
    public class TravelParticipant : BaseModel
    {
        [Required]
        public int TravelId { get; set; }

        [ForeignKey("TravelId")]
        public Travel? Travel { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
