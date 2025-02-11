using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Trip.Models
{
    public class UsefulLink
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TripId { get; set; }

        [ForeignKey("TripId")]
        public Trip Trip { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string URL { get; set; }

        [MaxLength(50)]
        public string Category { get; set; } // (Hotel, Trasporti, Ristoranti, Altro)
    }
}
