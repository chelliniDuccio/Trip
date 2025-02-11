using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Trip.Models;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TravelsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [EnableQuery] // Abilita OData su questa chiamata
        public ActionResult<IQueryable<Travel>> GetTravels()
        {
            return Ok(_context.Travels);
        }

        [HttpGet("{id}")]
        public ActionResult<Travel> GetTravel(int id)
        {
            var Travel = _context.Travels.Find(id);

            if (Travel == null)
            {
                return NotFound();
            }

            return Travel;
        }

        [HttpPost]
        public ActionResult<Travel> PostTravel(Travel Travel)
        {
            _context.Travels.Add(Travel);
            _context.SaveChanges();

            return CreatedAtAction("GetTravel", new { id = Travel.Id }, Travel);
        }

        [HttpPut("{id}")]
        public IActionResult PutTravel(int id, Travel Travel)
        {
            if (id != Travel.Id)
            {
                return BadRequest();
            }

            _context.Entry(Travel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTravel(int id)
        {
            var Travel = _context.Travels.Find(id);
            if (Travel == null)
            {
                return NotFound();
            }

            _context.Travels.Remove(Travel);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
