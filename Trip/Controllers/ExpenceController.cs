using Microsoft.AspNetCore.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : BaseController<Expense>
    {
        public ExpensesController(AppDbContext context) : base(context)
        {
        }
    }
}
