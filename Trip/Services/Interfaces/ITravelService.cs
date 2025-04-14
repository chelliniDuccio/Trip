using Trip.Models;
using Trip.Services.Interfaces.Extra;

namespace Trip.Services.Interfaces
{
    public interface ITravelService : IAuditableBaseService<Travel>
    {
        Task<List<Travel>> GetTravelsByIdsAsync(List<int> travelsId);
    }
}
