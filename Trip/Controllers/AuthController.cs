using Microsoft.AspNetCore.Mvc;
using Trip.Models.Extra;
using Trip.Services.Interfaces.Extra;

namespace Trip.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly AppDbContext _context;

        public AuthController(IJwtTokenService jwtTokenService, AppDbContext context)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Replace this with your actual user validation logic
            var user = _context.Users.SingleOrDefault(u => u.Username == request.Username);
            var token = _jwtTokenService.GenerateToken(user); // Example userId and role
            return Ok(new { Token = token });
        }
    }
}
