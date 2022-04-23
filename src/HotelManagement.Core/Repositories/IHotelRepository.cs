using HotelManagement.Core.Entities;
using HotelManagement.Core.Paging;
using HotelManagement.Core.Repositories.Base;

namespace HotelManagement.Core.Repositories
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<IEnumerable<Hotel>> GetHotelListAsync();
        Task<IPagedList<Hotel>> SearchHotelsAsync(PageSearchArgs args);
        Task<IEnumerable<Hotel>> GetHotelByNameAsync(string HotelName);
    }
}