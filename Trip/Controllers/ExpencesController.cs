using Microsoft.AspNetCore.Mvc;
using Trip.Controllers.Extra;
using Trip.Models;
using Trip.Models.Extra.DTOs;
using Trip.Services.Interfaces;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : AuditableController<Expense>
    {
        private readonly IExpensesService _expensesService;
        public ExpensesController(AppDbContext context, ILogger<BaseController<Expense>> logger, IExpensesService expensesService) : base(context, logger)
        {
            _expensesService = expensesService;
        }
        [HttpGet("stats/{travelId}")]
        public async Task<ExpenseStatsDto> GetTravelExpensesStats(int travelId)
        {
            try
            {
                var data = await _expensesService.GetTravelExpensesStatsAsync(travelId);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving expense statistics for travel ID {TravelId}", travelId);
                return new ExpenseStatsDto(); // Return an empty DTO or handle the error as needed
            }
        }
    }
}
