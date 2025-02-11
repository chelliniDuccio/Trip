using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Models
{
    public class SharedFile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TravelId { get; set; }

        [ForeignKey("TravelId")]
        public Travel Travel { get; set; }

        [Required]
        public int UploadedBy { get; set; }

        [ForeignKey("UploadedBy")]
        public User User { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string FileURL { get; set; }

        [Required]
        public DateTime UploadedAt { get; set; }
    }
}
