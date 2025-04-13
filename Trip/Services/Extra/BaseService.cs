using Trip.Models.Extra;
using Trip.Services.Interfaces.Extra;

namespace Trip.Services.Extra
{
    public class BaseService<T> : IBaseService<T> where T : BaseModel
    {
        protected readonly AppDbContext _context;

        public BaseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<T>> GetAllEntitiesAsync()
        {
            try
            {
                return await Task.FromResult(_context.Set<T>().AsQueryable());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> GetEntityFromIDAsync(int id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<T> CreateEntityAsync(T entity)
        {
            try
            {
                var createdEntity = (await _context.Set<T>().AddAsync(entity)).Entity;
                await _context.SaveChangesAsync();
                return createdEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<T> UpdateEntityAsync(T entity)
        {
            try
            {
                var updatedEntity = _context.Set<T>().Update(entity).Entity;
                await _context.SaveChangesAsync();
                return updatedEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteEntityAsync(int id)
        {
            try
            {
                var entity = await GetEntityFromIDAsync(id);

                if (entity == null)
                    return;

                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}