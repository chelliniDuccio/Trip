using System.ComponentModel.DataAnnotations;

namespace Trip.Models
{
    public class User : BaseModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
    }
}
