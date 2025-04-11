using Trip.Models.Extra;

namespace Trip.Services.Interfaces
{
    public interface IBaseService<T> where T : BaseModel
    {
        IQueryable<T> GetAllEntities();
        T GetEntityFromID(int id);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(int id);
    }
}
