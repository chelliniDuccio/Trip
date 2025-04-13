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
    }
}
