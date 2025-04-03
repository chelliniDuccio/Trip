using Trip.Models;

namespace Trip.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
    }
}
