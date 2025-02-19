using Microsoft.AspNetCore.Mvc;
using Trip.Controllers.Extra;
using Trip.Models;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : BaseController<Country>
    {
        public CountriesController(AppDbContext context) : base(context)
        {
        }
    }
}
