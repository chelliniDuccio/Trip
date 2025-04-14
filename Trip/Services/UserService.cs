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
