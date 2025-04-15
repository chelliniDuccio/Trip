using Microsoft.AspNetCore.Mvc;
using Trip.Models.Extra;
using Trip.Services.Interfaces;
using Trip.Services.Interfaces.Extra;

namespace Trip.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserService _userService;

        public AuthController(IJwtTokenService jwtTokenService, IUserService userService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.User) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "Email and password are required." });

            var user = await _userService.GetUserByUsernameOrEmailAsync(request.User);

            if (user == null)
                return Unauthorized(new { message = "Invalid email." });

            // Validate the password (#TODO passwords hashing)
            if (!string.Equals(_userService.HashPassword(request.Password), user.Password))
                return Unauthorized(new { message = "Invalid password." });

            var token = _jwtTokenService.GenerateToken(user);

            return Ok(new
            {
                token = token,
                user = new
                {
                    id = user.Id,
                    username = user.Username,
                    email = user.Email,
                    name = user.Name,
                    surname = user.Surname,
                    avatar = user.Avatar
                }
            });
        }
    }
}
