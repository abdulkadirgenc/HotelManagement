using HotelManagement.Core.Paging;
using HotelManagement.Infrastructure.Paging;
using HotelManagement.Infrastructure.Repository.Base;
using HotelManagement.Infrastructure.Data;
using System.Linq.Expressions;
using HotelManagement.Core.Entities;
using HotelManagement.Core.Repositories;
using HotelManagement.Core.Specifications;

namespace HotelManagement.Infrastructure.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelManagementContext context)
            : base(context)
        {
        }

        //public override async Task<Hotel> GetByIdAsync(int id)
        //{
        //    // first way
        //    return await base.GetByIdAsync(id);

        //    // second way
        //    //var spec = new HotelSpecification(id);
        //    //var hotels = await GetAsync(spec);
        //    //return hotels.FirstOrDefault();

        //    // third way
        //    //var hotels = await GetAsync(h => h.Id == id);
        //    //return hotels.FirstOrDefault();
        //}

        public async Task<IEnumerable<Hotel>> GetHotelListAsync()
        {
            var spec = new HotelSpecification();
            return await GetAsync(spec);

            // second way
            //return await ListAllAsync();
        }

        public Task<IPagedList<Hotel>> SearchHotelsAsync(PageSearchArgs args)
        {
            var query = Table;

            var orderByList = new List<Tuple<SortingOption, Expression<Func<Hotel, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Hotel, object>>>(sortingOption, h => h.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Hotel, object>>>(sortingOption, h => h.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<Hotel, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, h => h.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<Hotel, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Hotel, bool>>>(filteringOption, h => h.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Hotel, bool>>>(filteringOption, h => h.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var hotelPagedList = new PagedList<Hotel>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<Hotel>>(hotelPagedList);
        }

        public Task<IEnumerable<Hotel>> GetHotelByNameAsync(string HotelName)
        {
            throw new NotImplementedException();
        }
    }
}