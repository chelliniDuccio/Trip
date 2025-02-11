using System.ComponentModel.DataAnnotations;

namespace Trip.Models
{
    public class Travel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(255)]
        public string StayName { get; set; }
        [MaxLength(255)]
        public string StayAddress { get; set; }

        // Relazione con Expenses
        public ICollection<Expense> Expenses { get; set; }
    }
}
