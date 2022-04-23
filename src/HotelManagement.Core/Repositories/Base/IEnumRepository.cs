using HotelManagement.Core.Entities.Base;

namespace HotelManagement.Core.Repositories.Base
{
    public interface IEnumRepository<T> : IRepositoryBase<T, int> where T : IEntityBase<int>
    {
    }
}