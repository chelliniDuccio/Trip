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
        public UsersController(AppDbContext context) : base(context)
        {
        }

        [HttpPost]
        public override ActionResult<User> Post(User user)
        {
            if (_dbSet == null)
                return NotFound();

            user.CreationDate = DateTime.UtcNow;

            _dbSet.Add(user);
            _context.SaveChanges();
            return user;
        }

        [HttpPut("{id}")]
        public override ActionResult<User> Put(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            if (_dbSet == null)
                return NotFound();

            user.UpdateDate = DateTime.UtcNow;

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return user;
        }
    }
}
