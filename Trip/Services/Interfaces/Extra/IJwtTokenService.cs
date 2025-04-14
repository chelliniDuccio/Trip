using Trip.Models;

namespace Trip.Services.Interfaces.Extra
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
