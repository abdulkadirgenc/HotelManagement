using HotelManagement.Application.Models;
using HotelManagement.Core.Paging;

namespace HotelManagement.Application.Interfaces
{
    public interface IHotelRoomService
    {
        Task<IEnumerable<HotelRoomModel>> GetCheapestRoomPrices();
        Task<IPagedList<HotelRoomModel>> SearchHotelRooms(PageSearchArgs args);
    }
}
