using Trip.Models.Extra;

namespace Trip.Services.Interfaces.Extra
{
    public interface IBaseService<T> where T : BaseModel
    {
        Task<IQueryable<T>> GetAllEntitiesAsync();
        Task<T> GetEntityFromIDAsync(int id);
        Task<T> CreateEntityAsync(T entity);
        Task<T> UpdateEntityAsync(T entity);
        Task DeleteEntityAsync(int id);
    }
}
