using Trip.Models;
using Trip.Services.Extra;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class UsefulLinkService : BaseService<UsefulLink>, IUsefulLinkService
    {
        public UsefulLinkService(AppDbContext context) : base(context)
        {
        }
    }
}
