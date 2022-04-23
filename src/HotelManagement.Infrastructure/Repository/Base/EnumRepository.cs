using HotelManagement.Core.Entities.Base;
using HotelManagement.Core.Repositories.Base;
using HotelManagement.Infrastructure.Data;

namespace HotelManagement.Infrastructure.Repository.Base
{
    public class EnumRepository<T> : RepositoryBase<T, int>, IEnumRepository<T>
        where T : class, IEntityBase<int>
    {
        public EnumRepository(HotelManagementContext context)
            : base(context)
        {
        }
    }
}