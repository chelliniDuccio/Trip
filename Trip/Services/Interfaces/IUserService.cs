using Trip.Models;
using Trip.Services.Interfaces.Extra;

namespace Trip.Services.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<List<User?>> GetUsersByIdsAsync(List<int>? userId);
        Task<User?> GetUserByUsernameOrEmailAsync(string username);
    }
}
