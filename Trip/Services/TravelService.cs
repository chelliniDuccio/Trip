using Trip.Models;
using Trip.Services.Extra;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class TravelService : AuditableBaseService<Travel>, ITravelService
    {
        public TravelService(AppDbContext context) : base(context)
        {
        }
    }
}
