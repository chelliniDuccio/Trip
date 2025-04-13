using Trip.Models.Extra;

namespace Trip.Services.Interfaces.Extra
{
    public interface IAuditableBaseService<T> : IBaseService<T> where T : AuditableBaseModel
    {
    }
}
