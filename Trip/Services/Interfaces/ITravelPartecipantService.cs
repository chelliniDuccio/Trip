using Trip.Models;
using Trip.Services.Interfaces.Extra;

namespace Trip.Services.Interfaces
{
    public interface ITravelPartecipantService : IBaseService<TravelParticipant>
    {
        Task<List<Travel>> GetTravelsFromUserAsync(int userId);
        Task<List<User>> GetUsersFromTravelAsync(int travelId);
    }
}
