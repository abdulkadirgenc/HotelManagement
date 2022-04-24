using HotelManagement.Core.Entities;
using HotelManagement.Core.Paging;
using HotelManagement.Core.Repositories.Base;

namespace HotelManagement.Core.Repositories
{
    public interface IHotelRoomRepository : IRepository<HotelRoom>
    {
        Task<IEnumerable<HotelRoom>> GetCheapestRooms();
        Task<IPagedList<HotelRoom>> SearchHotelRooms(PageSearchArgs args);
        Task<IEnumerable<Tuple<Hotel, bool>>> RoomAvailabilityCheck(List<int> hotelIds, List<int> roomTypeIds, int requestedRoomCount);
    }
}
