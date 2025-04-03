using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Trip.Models.Extra.DTOs;
using Trip.Services;
using Trip.Services.Interfaces;

namespace Trip.Controllers.Extra
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected readonly AppDbContext _context;

        public AuthController(IAuthService authService, AppDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO request)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == request.Username);
            if (user == null || user.Password != request.Password) // Dovresti hashare la password
                return Unauthorized(new { message = "Invalid credentials" });

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
