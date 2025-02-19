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

        public BaseController(AppDbContext context)
        {
            _context = context;
            _dbSet = GetDbSet<T>();
        }

        [HttpGet]
        [EnableQuery] // Abilita OData su questa chiamata
        public virtual ActionResult<IQueryable<T>> Get()
        {
            if (_dbSet == null)
                return NotFound();

            return Ok(_dbSet);
        }

        [HttpGet("{id}")]
        public ActionResult<T> Get(int id)
        {
            if (_dbSet == null)
                return NotFound();

            var entity = _dbSet.Find(id);

            if (entity == null)
                return NotFound();

            return entity;
        }

        [HttpPost]
        public virtual ActionResult<T> Post(T entity)
        {
            if (_dbSet == null)
                return NotFound();

            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        [HttpPut("{id}")]
        public virtual ActionResult<T> Put(int id, T entity)
        {
            if (id != entity.Id)
                return BadRequest();

            if (_dbSet == null)
                return NotFound();

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_dbSet == null)
                return NotFound();

            var entity = _dbSet.Find(id);

            if (entity == null)
                return NotFound();

            _dbSet.Remove(entity);
            _context.SaveChanges();

            return NoContent();
        }

        private DbSet<T>? GetDbSet<T>() where T : BaseModel
        {
            var property = _context.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>));
            return property?.GetValue(_context) as DbSet<T>;
        }
    }
}
