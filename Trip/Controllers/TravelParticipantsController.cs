using Microsoft.AspNetCore.Mvc;
using Trip.Controllers.Extra;
using Trip.Models;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelParticipantsController : BaseController<TravelParticipant>
    {
        public TravelParticipantsController(AppDbContext context, ILogger<BaseController<TravelParticipant>> logger) : base(context, logger)
        {
        }
    }
}
