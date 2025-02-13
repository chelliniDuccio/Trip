using System.ComponentModel.DataAnnotations;
using Trip.Models.Extra;

namespace Trip.Models
{
    public class Travel : AuditableModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [MaxLength(255)]
        public string? StayAddress { get; set; }

        // Relazione con Expenses
        public ICollection<Expense>? Expenses { get; set; }
    }
}
