using HotelManagement.Core.Entities.Base;

namespace HotelManagement.Core.Repositories.Base;

public interface IRepository<T> : IRepositoryBase<T, int> where T : IEntityBase<int>
{
}
