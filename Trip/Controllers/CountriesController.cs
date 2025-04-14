using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Trip.Models;
using Trip.Services.Interfaces;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly ICountriesService _countriesService;
        public CountriesController(ILogger<CountriesController> logger, ICountriesService countriesService)
        {
            _logger = logger;
            _countriesService = countriesService;
        }

        [HttpGet()]
        [EnableQuery]
        public async Task<ActionResult<List<Country>>> GetAllCountries()
        {
            try
            {
                var countries = await _countriesService.GetAllEntitiesAsync();
                return Ok(countries.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving data for Country");
                return StatusCode(500, "An error occurred while retrieving data.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            try
            {
                var country = await _countriesService.GetEntityFromIDAsync(id);

                if (country == null)
                    return NotFound($"Country with ID {id} not found.");

                return Ok(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving data for Country with ID {id}");
                return StatusCode(500, "An error occurred while retrieving data.");
            }
        }
    }
}
