using HotelManagement.Application.Models;
using HotelManagement.Core.Paging;

namespace HotelManagement.Application.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelModel>> GetHotelList();
        Task<IPagedList<HotelModel>> SearchHotels(PageSearchArgs args);
        Task<HotelModel> GetHotelById(int hotelId);
        Task<IEnumerable<HotelModel>> GetHotelsByName(string name);
        Task<HotelModel> CreateHotel(HotelModel hotel);
        Task UpdateHotel(HotelModel hotel);
        Task DeleteHotelById(int hotelId);
    }
}