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
        private IHotelRepository _hotelRepository;

        public HotelRoomRepository(
            HotelManagementContext context,
            IHotelRepository hotelRepository
            ) : base(context)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<IEnumerable<HotelRoom>> GetCheapestRooms()
        {
            var minPriceByHotelId = from hr in Table
                                    group hr by hr.HotelId into mp
                                    select new { HotelId = mp.Key, Price = mp.Min(x => x.Price) };

            var hotelRooms = from hr in Table.Include(hr => hr.Hotel).Include(hr => hr.RoomType)
                             from mp in minPriceByHotelId
                             where hr.HotelId == mp.HotelId && hr.Price == mp.Price
                             orderby mp.Price, hr.Hotel.Name
                             select hr;

            return await hotelRooms.ToListAsync();
        }

        public Task<IPagedList<HotelRoom>> SearchHotelRooms(PageSearchArgs args)
        {
            var query = Table.Include(hr => hr.Hotel).Include(hr => hr.RoomType);

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
                        case "HotelName":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<HotelRoom, object>>>(sortingOption, hr => hr.Hotel.Name));
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
                        case "RoomTypeIds":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<HotelRoom, bool>>>(filteringOption, hr => ((List<int>)filteringOption.Value).Contains(hr.RoomTypeId)));
                            break;
                        case "HotelIds":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<HotelRoom, bool>>>(filteringOption, hr => ((List<int>)filteringOption.Value).Contains(hr.HotelId)));
                            break;
                    }
                }
            }

            var hotelPagedList = new PagedList<HotelRoom>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<HotelRoom>>(hotelPagedList);
        }

        public async Task<IEnumerable<Tuple<Hotel, bool>>> RoomAvailabilityCheck(List<int> hotelIds, List<int> roomTypeIds, int requestedRoomCount)
        {
            var hotelRoomQuery = from hr in Table
                                 where roomTypeIds.Contains(hr.RoomTypeId)
                                 select hr;

            if (hotelIds != null && hotelIds.Count > 0)
            {
                hotelRoomQuery = hotelRoomQuery.Where(hr => hotelIds.Contains(hr.HotelId));
            }

            var availableRoomCountByHotelId = from hr in hotelRoomQuery
                                              group hr by hr.HotelId into arc
                                              select new { HotelId = arc.Key, AvailableRoomCount = arc.Sum(x => x.MaxAllotment - x.SoldAllotment) };

            var hotelRooms = from hotel in _hotelRepository.Table
                             from arc in availableRoomCountByHotelId
                             where arc.HotelId == hotel.Id
                             select new Tuple<Hotel, bool>(hotel, arc.AvailableRoomCount >= requestedRoomCount);

            return await hotelRooms.ToListAsync();
        }
    }
}
