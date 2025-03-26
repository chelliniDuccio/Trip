using Microsoft.AspNetCore.Mvc;
using Trip.Controllers.Extra;
using Trip.Models;
using Trip.Models.Extra.DTOs;

namespace Trip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedFilesController : AuditableController<SharedFile>
    {
        public SharedFilesController(AppDbContext context, ILogger<BaseController<SharedFile>> logger) : base(context, logger)
        {
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

                _context.SharedFiles.Add(sharedFile);
                await _context.SaveChangesAsync();
            }

            return Ok("File salvato con successo.");
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = await _context.SharedFiles.FindAsync(id);
            if (file == null) return NotFound();

            return File(file.FileData, "application/octet-stream", file.FileName);
        }
    }
}
