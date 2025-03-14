using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trip.Models.Enums;
using Trip.Models.Extra;

namespace Trip.Models
{
    public class UsefulLink : AuditableModel
    {
        [Required]
        public int TravelId { get; set; }

        [ForeignKey("TravelId")]
        public Travel Travel { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string URL { get; set; }

        public CategoryType? Category { get; set; }

    }
}
