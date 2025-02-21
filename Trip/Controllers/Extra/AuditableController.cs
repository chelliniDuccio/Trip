using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trip.Models.Extra;

namespace Trip.Controllers.Extra
{
    public class AuditableController<T> : BaseController<T> where T : AuditableModel
    {
        public AuditableController(AppDbContext context, ILogger<BaseController<T>> logger) : base(context, logger)
        {
        }

        [HttpPost]
        public override ActionResult<T> Post(T entity)
        {
            try
            {
                if (_dbSet == null)
                    return NotFound("Database table not found.");

                entity.CreationAt = DateTime.UtcNow;

                // TODO: Se hai un sistema di autenticazione, puoi recuperare l'ID dell'utente così:
                // entity.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _dbSet.Add(entity);
                _context.SaveChanges();

                return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating entity {_entity}");
                return StatusCode(500, "An error occurred while creating the entity.");
            }
        }

        [HttpPut("{id}")]
        public override ActionResult<T> Put(int id, T entity)
        {
            if (id != entity.Id)
                return BadRequest($"{_entity} ID mismatch.");

            try
            {
                if (_dbSet == null)
                    return NotFound("Database table not found.");

                entity.UpdatedAt = DateTime.UtcNow;

                // TODO: Se hai un sistema di autenticazione, puoi recuperare l'ID dell'utente così:
                // entity.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(entity);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Concurrency error while updating {_entity} with ID {id}");
                return Conflict("A concurrency conflict occurred.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating entity {_entity} with ID {id}");
                return StatusCode(500, "An error occurred while updating the entity.");
            }
        }
    }
}
