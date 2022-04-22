using HotelManagement.Core.Entities.Base;
using HotelManagement.Core.Repositories.Base;
using HotelManagement.Infrastructure.Data;

namespace HotelManagement.Infrastructure.Repository.Base;

public class Repository<T> : RepositoryBase<T, int>, IRepository<T>
    where T : class, IEntityBase<int>
{
    public Repository(HotelManagementContext context)
        : base(context)
    {
    }
}
