using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Models.Extra
{
    public class AuditableModel : BaseModel
    {
        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreationAt { get; set; }

        [ForeignKey("CreatedBy")]
        public User? CreatedByUser { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("UpdatedBy")]
        public User? UpdatedByUser { get; set; }
    }
}
