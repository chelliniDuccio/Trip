using Trip.Models;
using Trip.Services.Extra;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class CoutrieService : BaseService<Country>, ICountriesService
    {
        public CoutrieService(AppDbContext context) : base(context)
        {
        }
    }
}
