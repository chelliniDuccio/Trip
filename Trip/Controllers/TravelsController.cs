using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Trip.Models;
using Trip.Services.Interfaces;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelsController : ControllerBase
    {
        private readonly ITravelService _travelService;
        private readonly ILogger<TravelsController> _logger;

        public TravelsController(ITravelService travelService, ILogger<TravelsController> logger)
        {
            _travelService = travelService;
            _logger = logger;
        }

        [HttpGet()]
        [EnableQuery]
        public async Task<ActionResult<List<Travel>>> GetAllTravels()
        {
            try
            {
                var travels = await _travelService.GetAllEntitiesAsync();
                return Ok(travels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all travels");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Travel>> GetTravel(int id)
        {
            try
            {
                var travel = await _travelService.GetEntityFromIDAsync(id);
                if (travel == null)
                    return NotFound($"Travel with ID {id} not found.");
                return Ok(travel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving travel with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Travel>> CreateTravel([FromBody] Travel travel)
        {
            if (travel == null)
                return BadRequest("Travel is null.");
            try
            {
                var createdTravel = await _travelService.CreateEntityAsync(travel);
                return CreatedAtAction(nameof(GetTravel), new { id = createdTravel.Id }, createdTravel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating travel");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Travel>> UpdateTravel(int id, [FromBody] Travel travel)
        {
            if (travel == null)
                return BadRequest("Travel is null.");
            try
            {
                var updatedTravel = await _travelService.UpdateEntityAsync(travel);
                if (updatedTravel == null)
                    return NotFound($"Travel with ID {id} not found.");
                return Ok(updatedTravel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating travel with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTravel(int id)
        {
            try
            {
                var travel = await _travelService.GetEntityFromIDAsync(id);

                if (travel == null)
                    return NotFound($"Travel with ID {id} not found.");

                await _travelService.DeleteEntityAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting travel with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
