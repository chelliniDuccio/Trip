using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Trip.Models;
using Trip.Services.Interfaces;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelParticipantsController : ControllerBase
    {
        private readonly ITravelPartecipantService _travelPartecipantService;
        private readonly ILogger<TravelParticipantsController> _logger;

        public TravelParticipantsController(ITravelPartecipantService travelPartecipantService, ILogger<TravelParticipantsController> logger)
        {
            _travelPartecipantService = travelPartecipantService;
            _logger = logger;
        }

        [HttpGet()]
        [EnableQuery]
        public async Task<ActionResult<List<TravelParticipant>>> GetAllTravelParticipants()
        {
            try
            {
                var travelParticipants = await _travelPartecipantService.GetAllEntitiesAsync();
                return Ok(travelParticipants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all travel participants");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TravelParticipant>> GetTravelParticipant(int id)
        {
            try
            {
                var travelParticipant = await _travelPartecipantService.GetEntityFromIDAsync(id);

                if (travelParticipant == null)
                    return NotFound($"Travel participant with ID {id} not found.");

                return Ok(travelParticipant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving travel participant with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TravelParticipant>> CreateTravelParticipant([FromBody] TravelParticipant travelParticipant)
        {
            if (travelParticipant == null)
                return BadRequest("Travel participant is null.");
            try
            {
                var createdTravelPartecipant = await _travelPartecipantService.CreateEntityAsync(travelParticipant);
                return CreatedAtAction(nameof(GetTravelParticipant), new { id = createdTravelPartecipant.Id }, createdTravelPartecipant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating travel participant");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TravelParticipant>> UpdateTravelParticipant(int id, [FromBody] TravelParticipant travelParticipant)
        {
            if (travelParticipant == null)
                return BadRequest("Travel participant is null.");
            if (id != travelParticipant.Id)
                return BadRequest("ID mismatch.");
            try
            {
                var existingTravelParticipant = await _travelPartecipantService.GetEntityFromIDAsync(id);

                if (existingTravelParticipant == null)
                    return NotFound($"Travel participant with ID {id} not found.");

                await _travelPartecipantService.UpdateEntityAsync(travelParticipant);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating travel participant with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTravelParticipant(int id)
        {
            try
            {
                var travelParticipant = await _travelPartecipantService.GetEntityFromIDAsync(id);

                if (travelParticipant == null)
                    return NotFound($"Travel participant with ID {id} not found.");

                await _travelPartecipantService.DeleteEntityAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting travel participant with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
