using Trip.Models;
using Trip.Services.Extra;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            try
            {
                return await _context.Users.FindAsync(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<User?>> GetUsersByIdsAsync(List<int>? userId)
        {
            if (userId == null || !userId.Any())
                return new List<User?>();

            var users = await Task.WhenAll(userId.Select(GetUserByIdAsync));
            return users.ToList();
        }
    }
}
