using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trip.Models.Extra;

namespace Trip.Models
{
    public class SharedFile : AuditableModel
    {

        [Required]
        public int TravelId { get; set; }

        [ForeignKey("TravelId")]
        public Travel? Travel { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string FileURL { get; set; }
    }
}
