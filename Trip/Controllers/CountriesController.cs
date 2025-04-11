using Microsoft.AspNetCore.Mvc;
using Trip.Controllers.Extra;
using Trip.Models;
using Trip.Models.Extra.DTOs;
using Trip.Services;
using Trip.Services.Interfaces;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CountriesController> _logger;
        private readonly ICountriesService _countriesService;
        public CountriesController(AppDbContext context, ILogger<CountriesController> logger, ICountriesService countriesService)
        {
            _context = context;
            _logger = logger;
            _countriesService = countriesService;
        }

        [HttpGet()]
        public async Task<List<Country>> GetAllCountries()
        {
            try
            {
                var data = _countriesService.GetAllEntities().ToList();

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving expense statistics for travel ID");
                return null; // Return an empty DTO or handle the error as needed
            }
        }
    }

}
