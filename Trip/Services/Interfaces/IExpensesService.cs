using Trip.Models;
using Trip.Models.Extra.DTOs;
using Trip.Services.Interfaces.Extra;

namespace Trip.Services.Interfaces
{
    public interface IExpensesService : IAuditableBaseService<Expense>
    {
        Task<ExpenseStatsDto> GetTravelExpensesStatsAsync(int travelId);
        Task<List<CurrencyTableDTO>> GetCurrencyTablesAsync();
    }
}
