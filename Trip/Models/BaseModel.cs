using System.ComponentModel.DataAnnotations;

namespace Trip.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
