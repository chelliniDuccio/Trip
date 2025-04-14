using Trip.Models;
using Trip.Services.Extra;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class CountryService : BaseService<Country>, ICountriesService
    {
        public CountryService(AppDbContext context) : base(context)
        {
        }
    }
}
