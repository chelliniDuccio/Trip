using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Trip.Models.Extra;

namespace Trip.Models
{
    public class User : BaseModel
    {
        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        public string Avatar => $"{Name.ToUpper()[0]}{Surname.ToUpper()[0]}";
    }
}
