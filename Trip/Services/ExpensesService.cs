using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trip.Models.Extra.DTOs;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ExpensesService> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ExpensesService(AppDbContext context, ILogger<ExpensesService> logger, IUserService userService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ExpenseStatsDto> GetTravelExpensesStatsAsync(int travelId)
        {
            try
            {
                var stats = await GetGroupedExpensesAsync(travelId);
                if (!stats.Any()) return new ExpenseStatsDto(); // Avoid unnecessary processing

                var maxAmount = stats.Max(x => x.AmountSum);
                var topPayers = stats.Where(x => x.AmountSum == maxAmount).ToList();
                var usersDto = await GetMappedUsersAsync(topPayers);

                return new ExpenseStatsDto
                {
                    TravelExpenses = new TravelExpense(topPayers.First()), // Safe due to Any() check
                    TotalAmount = stats.Sum(x => x.AmountSum),
                    Users = usersDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving expense statistics for travel ID {TravelId}", travelId);
                throw;
            }
        }

        private async Task<List<TravelTotalExpense>> GetGroupedExpensesAsync(int travelId)
        {
            return await _context.Expenses
                .Where(e => e.TravelId == travelId)
                .GroupBy(e => new { e.PaidBy, e.Currency, e.CurrencySymbol })
                .Select(g => new TravelTotalExpense
                {
                    PaidBy = g.Key.PaidBy,
                    AmountSum = g.Sum(e => e.Amount),
                    Currency = g.Key.Currency,
                    CurrencySymbol = g.Key.CurrencySymbol
                })
                .ToListAsync();
        }

        private async Task<List<UserDto>> GetMappedUsersAsync(List<TravelTotalExpense> topPayers)
        {
            var userIds = topPayers.Select(x => x.PaidBy).ToList();
            var users = await _userService.GetUsersByIdsAsync(userIds);
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
