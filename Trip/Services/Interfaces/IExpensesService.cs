using Trip.Models.Extra.DTOs;

namespace Trip.Services.Interfaces
{
    public interface IExpensesService
    {
        Task<ExpenseStatsDto> GetTravelExpensesStatsAsync(int travelId);
    }
}
