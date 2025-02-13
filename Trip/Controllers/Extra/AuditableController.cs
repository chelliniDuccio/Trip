using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trip.Models.Extra;

namespace Trip.Controllers.Extra
{
    public class AuditableController<T> : BaseController<T> where T : AuditableModel
    {
        public AuditableController(AppDbContext context) : base(context)
        {
        }

        [HttpPost]
        public override ActionResult<T> Post(T entity)
        {
            if (_dbSet == null)
                return NotFound();

            entity.CreationDate = DateTime.UtcNow;
            //entity.CreationDate = userId; :TODO

            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        [HttpPut("{id}")]
        public override ActionResult<T> Put(int id, T entity)
        {
            if (id != entity.Id)
                return BadRequest();

            if (_dbSet == null)
                return NotFound();

            entity.UpdateDate = DateTime.UtcNow;
            //entity.UpdatedBy = userId; :TODO

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }
    }
}
