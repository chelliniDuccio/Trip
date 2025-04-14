using Microsoft.AspNetCore.Mvc;
using Trip.Models;
using Trip.Models.Extra.DTOs;
using Trip.Services.Interfaces;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedFilesController : ControllerBase
    {
        private readonly ISharedFilesService _sharedFilesService;
        private readonly ILogger<SharedFilesController> _logger;

        public SharedFilesController(ISharedFilesService sharedFilesService, ILogger<SharedFilesController> logger)
        {
            _sharedFilesService = sharedFilesService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<List<SharedFile>>> GetAllFiles()
        {
            try
            {
                var files = await _sharedFilesService.GetAllEntitiesAsync();
                return Ok(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all files");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SharedFile>> GetFile(int id)
        {
            try
            {
                var file = await _sharedFilesService.GetEntityFromIDAsync(id);

                if (file == null)
                    return NotFound($"File with ID {id} not found.");

                return Ok(file);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving file with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] SharedFileImportDto fileDto)
        {
            if (fileDto.File == null || fileDto.File.Length == 0)
                return BadRequest("File non valido.");

            using (var ms = new MemoryStream())
            {
                await fileDto.File.CopyToAsync(ms);
                var fileData = ms.ToArray();

                var sharedFile = new SharedFile
                {
                    TravelId = fileDto.TravelId,
                    FileName = fileDto.FileName,
                    FileData = fileData,
                    Category = fileDto.Category,
                    CreatedBy = 1,
                    CreationAt = DateTime.UtcNow
                };

                await _sharedFilesService.CreateEntityAsync(sharedFile);
            }

            return Ok("File salvato con successo.");
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = await _sharedFilesService.GetEntityFromIDAsync(id);

            if (file == null)
                return NotFound();

            return File(file.FileData, "application/pdf", file.FileName);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            try
            {
                await _sharedFilesService.DeleteEntityAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
