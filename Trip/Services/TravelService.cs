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

        public async Task<List<Travel>> GetTravelsByIdsAsync(List<int> travelIds)
        {
            if (travelIds == null || !travelIds.Any())
                return new List<Travel>();

            var users = await Task.WhenAll(travelIds.Select(GetEntityFromIDAsync));
            return users.ToList();
        }
    }
}
