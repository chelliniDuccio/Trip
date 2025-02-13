using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Models.Extra
{
    public class AuditableModel : BaseModel
    {
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public User? CreatedByUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public User? UpdatedByUser { get; set; }
    }

}
