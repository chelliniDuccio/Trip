using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Trip.Models;
using Trip.Models.Extra.DTOs;
using Trip.Services.Interfaces;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesService _expensesService;
        private readonly ILogger<ExpensesController> _logger;

        public ExpensesController(IExpensesService expensesService, ILogger<ExpensesController> logger)
        {
            _expensesService = expensesService;
            _logger = logger;
        }

        [HttpGet()]
        [EnableQuery]
        public async Task<ActionResult<List<Expense>>> GetAllExpenses()
        {
            try
            {
                var expenses = await _expensesService.GetAllEntitiesAsync();
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all expenses");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            try
            {
                var expense = await _expensesService.GetEntityFromIDAsync(id);

                if (expense == null)
                    return NotFound($"Expense with ID {id} not found.");

                return Ok(expense);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving expense with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> CreateExpense(Expense expense)
        {
            try
            {
                if (expense == null)
                    return BadRequest("Expense cannot be null.");

                var createdExpense = await _expensesService.CreateEntityAsync(expense);
                return CreatedAtAction(nameof(GetExpense), new { id = createdExpense.Id }, createdExpense);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating expense");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Expense>> UpdateExpense(int id, Expense expense)
        {
            try
            {
                if (id != expense.Id)
                    return BadRequest("Expense ID mismatch.");

                var existingExpense = await _expensesService.GetEntityFromIDAsync(id);

                if (existingExpense == null)
                    return NotFound($"Expense with ID {id} not found.");

                await _expensesService.UpdateEntityAsync(expense);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating expense with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense(int id)
        {
            try
            {
                var existingExpense = await _expensesService.GetEntityFromIDAsync(id);

                if (existingExpense == null)
                    return NotFound($"Expense with ID {id} not found.");

                await _expensesService.DeleteEntityAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting expense with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
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
                return new ExpenseStatsDto();
            }
        }

        [HttpGet("currency-tables")]
        public async Task<ActionResult<List<CurrencyTableDTO>>> GetCurrencyTables()
        {
            try
            {
                var data = await _expensesService.GetCurrencyTablesAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving currency tables");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
