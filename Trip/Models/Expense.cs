using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trip.Models.Enums;
using Trip.Models.Extra;

namespace Trip.Models
{
    public class Expense : AuditableModel
    {
        [Required]
        public decimal Amount { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int TravelId { get; set; }

        [ForeignKey("TravelId")]
        public Travel? Travel { get; set; }

        [Required]
        [MaxLength(10)]
        public string Currency { get; set; }

        [MaxLength(5)]
        public string? CurrencySymbol { get; set; }

        [Required]
        public int PaidBy { get; set; }

        [ForeignKey("PaidBy")]
        public User? PaidByUser { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }
        public CategoryType? Category { get; set; }
    }
}