using HotelManagement.Core.Entities;
using HotelManagement.Infrastructure.Repository.Base;
using HotelManagement.Infrastructure.Data;
using HotelManagement.Core.Repositories;

namespace HotelManagement.Infrastructure.Repository
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {

        public ReservationRepository(HotelManagementContext context) : base(context)
        {
        }
    }
}
