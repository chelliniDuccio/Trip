using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Trip.Models;
using Trip.Services.Interfaces;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _usersService = userService;
            _logger = logger;
        }

        [HttpGet()]
        [EnableQuery]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                try
                {
                    var users = await _usersService.GetAllEntitiesAsync();
                    return Ok(users.ToList());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error retrieving data for Country");
                    return StatusCode(500, "An error occurred while retrieving data.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _usersService.GetEntityFromIDAsync(id);

                if (user == null)
                    return NotFound($"User with ID {id} not found.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving data for User with ID {id}");
                return StatusCode(500, "An error occurred while retrieving data.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("User is null.");
            try
            {
                user.Password = _usersService.HashPassword(user.Password);
                var createdUser = await _usersService.CreateEntityAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || id != user.Id)
                return BadRequest("User is null or ID mismatch.");
            try
            {
                var existingUser = await _usersService.GetEntityFromIDAsync(id);
                if (existingUser == null)
                    return NotFound($"User with ID {id} not found.");
                await _usersService.UpdateEntityAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user with ID {id}");
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }
    }
}
