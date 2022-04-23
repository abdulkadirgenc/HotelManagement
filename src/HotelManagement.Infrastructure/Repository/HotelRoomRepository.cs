using HotelManagement.Core.Entities;
using HotelManagement.Infrastructure.Repository.Base;
using HotelManagement.Infrastructure.Data;
using HotelManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Core.Paging;
using System.Linq.Expressions;
using HotelManagement.Infrastructure.Paging;

namespace HotelManagement.Infrastructure.Repository
{
    public class HotelRoomRepository : Repository<HotelRoom>, IHotelRoomRepository
    {
        public HotelRoomRepository(HotelManagementContext context) : base(context)
        {
        }

        public async Task<IEnumerable<HotelRoom>> GetCheapestRooms()
        {
            var minPriceByHotelId = from hr in Table
                                    group hr by hr.HotelId into mp
                                    select new { HotelId = mp.Key, Price = mp.Min(x => x.Price) };

            var hotelRooms = from hr in Table.Include(hr => hr.Hotel).Include(hr => hr.RoomType)
                             from mp in minPriceByHotelId
                             where hr.HotelId == mp.HotelId && hr.Price == mp.Price
                             orderby mp.Price descending, hr.Hotel.Name
                             select hr;

            return await hotelRooms.ToListAsync();
        }


        public Task<IPagedList<HotelRoom>> AdvancedRoomSearch(PageSearchArgs args)
    {
            var query = Table;

            var orderByList = new List<Tuple<SortingOption, Expression<Func<HotelRoom, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "Price":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<HotelRoom, object>>>(sortingOption, hr => hr.Price));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<HotelRoom, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, hr => hr.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<HotelRoom, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "RoomTypeId":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<HotelRoom, bool>>>(filteringOption, hr => ((List<int>)filteringOption.Value).Contains(hr.RoomTypeId)));
                            break;
                        case "HotelId":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<HotelRoom, bool>>>(filteringOption, hr => ((List<int>)filteringOption.Value).Contains(hr.HotelId)));
                            break;
                    }
                }
            }

            var hotelPagedList = new PagedList<HotelRoom>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<HotelRoom>>(hotelPagedList);
        }
    }
}
