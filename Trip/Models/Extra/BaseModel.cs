using System.ComponentModel.DataAnnotations;

namespace Trip.Models.Extra
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
