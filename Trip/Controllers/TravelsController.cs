using Microsoft.AspNetCore.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelsController : BaseController<Travel>
    {
        public TravelsController(AppDbContext context) : base(context)
        {
        }
    }
}
