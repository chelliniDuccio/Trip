using Trip.Models;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            try
            {
                return await _context.Users.FindAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID {UserId}", userId);
                throw;
            }
        }

        public async Task<List<User?>> GetUsersByIdsAsync(List<int> userId)
        {
            var result = new List<User?>();

            foreach (var id in userId)
            {
                var user = await GetUserByIdAsync(id);
                result.Add(user);
            }

            return result;
        }
    }
}
