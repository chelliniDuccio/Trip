using Trip.Models;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class CoutriesService : BaseService<Country>, ICountriesService
    {
        public CoutriesService(AppDbContext context, ILogger<BaseService<Country>> logger) : base(context, logger)
        {
        }
    }
}
