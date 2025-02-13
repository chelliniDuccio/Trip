using Microsoft.AspNetCore.Mvc;
using Trip.Controllers.Extra;
using Trip.Models;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelsController : AuditableController<Travel>
    {
        public TravelsController(AppDbContext context) : base(context)
        {
        }
    }
}
