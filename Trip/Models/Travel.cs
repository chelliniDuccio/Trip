using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using Trip.Models.Extra;

namespace Trip.Models
{
    public class Travel : AuditableBaseModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country? Country { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [MaxLength(255)]
        public string? StayAddress { get; set; }
        public string? StayURL => getStayURL();

        // Relazione con Expenses
        public ICollection<Expense>? Expenses { get; set; }

        private string getStayURL()
        {
            if (!string.IsNullOrEmpty(StayAddress))
                return $"https://www.google.com/maps/embed/v1/place?key={Constants.GoogleApiKey}&q={HttpUtility.UrlEncode(StayAddress)}";
            return null;
        }
    }
}
