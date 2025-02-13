using Microsoft.AspNetCore.Mvc;
using Trip.Controllers.Extra;
using Trip.Models;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : AuditableController<Expense>
    {
        public ExpensesController(AppDbContext context) : base(context)
        {
        }
    }
}
