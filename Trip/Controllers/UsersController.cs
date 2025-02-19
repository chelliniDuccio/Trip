using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trip.Controllers.Extra;
using Trip.Models;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User>
    {
        public UsersController(AppDbContext context, ILogger<BaseController<User>> logger) : base(context, logger)
        {
        }

        [HttpPost]
        public override ActionResult<User> Post(User user)
        {
            try
            {
                if (_dbSet == null)
                    return NotFound("Database table not found.");

                user.CreationDate = DateTime.UtcNow;

                // TODO: Se vuoi salvare l'utente che crea l'account, recupera l'ID dell'utente autenticato:
                // user.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _dbSet.Add(user);
                _context.SaveChanges();

                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating user {user.Id}");
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [HttpPut("{id}")]
        public override ActionResult<User> Put(int id, User user)
        {
            if (id != user.Id)
                return BadRequest("Users ID mismatch.");

            try
            {
                if (_dbSet == null)
                    return NotFound("Database table not found.");

                user.UpdateDate = DateTime.UtcNow;

                // TODO: Se vuoi salvare l'utente che aggiorna il profilo, recupera l'ID dell'utente autenticato:
                // user.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(user);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Concurrency error while updating user {id}");
                return Conflict("A concurrency conflict occurred.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user {id}");
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }
    }
}
