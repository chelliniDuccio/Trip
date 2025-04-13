using Trip.Models;
using Trip.Services.Extra;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class TravelService : BaseService<Travel>, ITravelService
    {
        public TravelService(AppDbContext context) : base(context)
        {
        }
    }
}
