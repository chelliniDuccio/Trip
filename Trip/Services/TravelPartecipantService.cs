using Microsoft.EntityFrameworkCore;
using Trip.Models;
using Trip.Services.Extra;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class TravelPartecipantService : BaseService<TravelParticipant>, ITravelPartecipantService
    {
        public TravelPartecipantService(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Travel>> GetTravelsFromUserAsync(int userId)
        {
            try
            {
                var travelsFromUser = await _context.TravelParticipants
                    .Where(tp => tp.UserId == userId)
                    .Include(tp => tp.Travel) // Include the Travel navigation property
                        .ThenInclude(t => t.Country) // Include the Country navigation property
                    .Select(tp => tp.Travel) // Project to Travel
                    .ToListAsync();

                return travelsFromUser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<User>> GetUsersFromTravel(int travelId)
        {
            try
            {
                var travelsFromUser = GetAllEntitiesAsync().Result.Where(x => x.TravelId == travelId).Select(x => x.User);

                return travelsFromUser.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
