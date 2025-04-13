using Trip.Models.Extra;
using Trip.Services.Interfaces.Extra;

namespace Trip.Services.Extra
{
    public class AuditableBaseService<T> : BaseService<T>, IAuditableBaseService<T>  where T : AuditableBaseModel
    {
        public AuditableBaseService(AppDbContext context) : base(context)
        {
        }

        public override async Task<T> CreateEntityAsync(T entity)
        {
            try
            {
                entity.CreationAt = DateTime.UtcNow;
                return await base.CreateEntityAsync(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override async Task<T> UpdateEntityAsync(T entity)
        {
            try
            {
                entity.UpdatedAt = DateTime.UtcNow;
                return await base.UpdateEntityAsync(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
