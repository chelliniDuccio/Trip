using Trip.Models;
using Trip.Services.Extra;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class SharedFileService : AuditableBaseService<SharedFile>, ISharedFilesService
    {
        public SharedFileService(AppDbContext context) : base(context)
        {
        }
    }
}
