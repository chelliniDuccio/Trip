using System.ComponentModel.DataAnnotations;
using Trip.Models.Enums;

namespace Trip.Models.Extra.DTOs
{
    public class SharedFileImportDto
    {
        [Required]
        public int TravelId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public IFormFile File { get; set; } // Il file da caricare

        public CategoryType? Category { get; set; }
    }
}
