using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> GetUserByUsernameOrEmailAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username || x.Email == username);
        }

        public async Task<List<User?>> GetUsersByIdsAsync(List<int>? userId)
        {
            if (userId == null || !userId.Any())
                return new List<User?>();

            var users = await Task.WhenAll(userId.Select(GetUserByIdAsync));
            return users.ToList();
        }

        public string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
