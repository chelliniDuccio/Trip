using Microsoft.AspNetCore.Mvc;
using Trip.Models.Extra;
using Trip.Services.Interfaces;

namespace Trip.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BaseService<T>> _logger;

        public BaseService(AppDbContext context, ILogger<BaseService<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<T> GetAllEntities()
        {
            try
            {
                return _context.Set<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all entities");
                throw;
            }
        }

        public T GetEntityFromID(int id)
        {
            try
            {
                var entity = _context.Set<T>().FirstOrDefault(x => x.Id == id);
                if (entity == null)
                {
                    _logger.LogWarning($"Entity with ID {id} not found.");
                    return null;
                }
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving entity with ID {id}");
                throw;
            }
        }

        public void AddEntity(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding entity");
                throw;
            }
        }

        public void UpdateEntity(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity");
                throw;
            }
        }

        public void DeleteEntity(int id)
        {
            try
            {
                var entity = _context.Set<T>().Find(id);
                if (entity == null)
                {
                    _logger.LogWarning($"Entity with ID {id} not found.");
                    return;
                }
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entity");
                throw;
            }
        }
    }
}