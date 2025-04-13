using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trip.Models.Enums;
using Trip.Models.Extra;

namespace Trip.Models
{
    public class SharedFile : AuditableBaseModel
    {
        [Required]
        public int TravelId { get; set; }

        [ForeignKey("TravelId")]
        public Travel? Travel { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [Required]
        public byte[] FileData { get; set; }  // Qui viene salvato il file come BLOB

        public CategoryType? Category { get; set; }
    }
}

