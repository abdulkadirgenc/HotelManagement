using HotelManagement.Infrastructure.Repository.Base;
using HotelManagement.Infrastructure.Data;
using HotelManagement.Core.Entities;
using HotelManagement.Core.Repositories;

namespace HotelManagement.Infrastructure.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelManagementContext context)
            : base(context)
        {
        }
    }
}
