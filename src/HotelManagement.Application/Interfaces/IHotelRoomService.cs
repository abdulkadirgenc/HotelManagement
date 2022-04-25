using HotelManagement.Application.Models;
using HotelManagement.Application.Models.Entity;
using HotelManagement.Core.Paging;

namespace HotelManagement.Application.Interfaces
{
    public interface IHotelRoomService
    {
        Task<IEnumerable<HotelRoomModel>> GetCheapestRoomPrices();
        Task<IPagedList<HotelRoomModel>> SearchHotelRooms(PageSearchArgs args);
        Task<IEnumerable<HotelRoomAvailabilityModel>> RoomAvailabilityCheck(AvailabilitySearchArgs args);
    }
}
