using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Trip.Models;
using Trip.Services.Interfaces;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsefulLinksController : ControllerBase
    {
        private readonly IUsefulLinkService _usefulLinkService;
        private readonly ILogger<UsefulLinksController> _logger;

        public UsefulLinksController(IUsefulLinkService usefulLinkService, ILogger<UsefulLinksController> logger)
        {
            _usefulLinkService = usefulLinkService;
            _logger = logger;
        }

        [HttpGet()]
        [EnableQuery]
        public async Task<ActionResult<List<UsefulLink>>> GetAllUsefulLinks()
        {
            try
            {
                var usefulLinks = await _usefulLinkService.GetAllEntitiesAsync();
                return Ok(usefulLinks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all useful links");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsefulLink>> GetUsefulLink(int id)
        {
            try
            {
                var usefulLink = await _usefulLinkService.GetEntityFromIDAsync(id);

                if (usefulLink == null)
                    return NotFound($"Useful link with ID {id} not found.");

                return Ok(usefulLink);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving useful link with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsefulLink>> CreateUsefulLink([FromBody] UsefulLink usefulLink)
        {
            if (usefulLink == null)
                return BadRequest("Useful link is null.");
            try
            {
                var createdUsefulLink = await _usefulLinkService.CreateEntityAsync(usefulLink);
                return CreatedAtAction(nameof(GetUsefulLink), new { id = createdUsefulLink.Id }, createdUsefulLink);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating useful link");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsefulLink>> UpdateUsefulLink(int id, [FromBody] UsefulLink usefulLink)
        {
            if (usefulLink == null || id != usefulLink.Id)
                return BadRequest("Useful link is null or ID mismatch.");
            try
            {
                var updatedUsefulLink = await _usefulLinkService.UpdateEntityAsync(usefulLink);
                return Ok(updatedUsefulLink);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating useful link with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsefulLink(int id)
        {
            try
            {
                var existingUsefulLink = await _usefulLinkService.GetEntityFromIDAsync(id);
                if (existingUsefulLink == null)
                    return NotFound($"Useful link with ID {id} not found.");

                await _usefulLinkService.DeleteEntityAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting useful link with ID {id}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
