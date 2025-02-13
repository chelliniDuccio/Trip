using Microsoft.AspNetCore.Mvc;
using Trip.Controllers.Extra;
using Trip.Models;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedFilesController : AuditableController<SharedFile>
    {
        public SharedFilesController(AppDbContext context) : base(context)
        {
        }
    }
}
