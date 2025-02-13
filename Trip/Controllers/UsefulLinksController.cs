using Microsoft.AspNetCore.Mvc;
using Trip.Controllers.Extra;
using Trip.Models;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsefulLinksController : AuditableController<UsefulLink>
    {
        public UsefulLinksController(AppDbContext context) : base(context)
        {
        }
    }
}
