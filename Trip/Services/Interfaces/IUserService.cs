using Trip.Models;

namespace Trip.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<List<User?>> GetUsersByIdsAsync(List<int> userId);
    }
}
