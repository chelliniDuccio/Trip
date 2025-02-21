using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Trip.Models.Extra;

namespace Trip.Controllers.Extra
{
    public class BaseController<T> : ControllerBase where T : BaseModel
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T>? _dbSet;
        protected readonly ILogger<BaseController<T>> _logger;
        protected readonly string _entity;

        public BaseController(AppDbContext context, ILogger<BaseController<T>> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = GetDbSet<T>();
            _entity = typeof(T).Name;
        }

        [HttpGet]
        [EnableQuery] // Abilita OData su questa chiamata
        public virtual ActionResult<IQueryable<T>> Get()
        {
            try
            {
                if (_dbSet == null)
                    return NotFound("Database table not found.");

                return Ok(_dbSet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving data for {_entity}");
                return StatusCode(500, "An error occurred while retrieving data.");
            }
        }

        [HttpGet("{id}")]
        public virtual ActionResult<T> Get(int id)
        {
            try
            {
                if (_dbSet == null)
                    return NotFound("Database table not found.");

                var entity = _dbSet.Find(id);
                if (entity == null)
                    return NotFound($"Entity with ID {id} not found.");

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving entity {_entity} with ID {id}");
                return StatusCode(500, "An error occurred while retrieving the entity.");
            }
        }

        [HttpPost]
        public virtual ActionResult<T> Post(T entity)
        {
            try
            {
                if (_dbSet == null)
                    return NotFound("Database table not found.");

                _dbSet.Add(entity);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating entity {Entity}", typeof(T).Name);
                return StatusCode(500, "An error occurred while creating the entity.");
            }
        }

        [HttpPut("{id}")]
        public virtual ActionResult<T> Put(int id, T entity)
        {
            if (id != entity.Id)
                return BadRequest($"{_entity} ID mismatch.");

            try
            {
                if (_dbSet == null)
                    return NotFound("Database table not found.");

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

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                if (_dbSet == null)
                    return NotFound("Database table not found.");

                var entity = _dbSet.Find(id);
                if (entity == null)
                    return NotFound($"{_entity}  with ID {id} not found.");

                _dbSet.Remove(entity);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting entity {_entity} with ID {id}");
                return StatusCode(500, "An error occurred while deleting the entity.");
            }
        }

        private DbSet<T>? GetDbSet<T>() where T : BaseModel
        {
            try
            {
                var property = _context.GetType().GetProperties()
                    .FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>));

                return property?.GetValue(_context) as DbSet<T>;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting DbSet for {Entity}", typeof(T).Name);
                return null;
            }
        }
    }
}
