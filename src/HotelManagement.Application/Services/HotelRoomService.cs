using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Mapper;
using HotelManagement.Application.Models;
using HotelManagement.Application.Models.Entity;
using HotelManagement.Core.Interfaces;
using HotelManagement.Core.Paging;
using HotelManagement.Core.Repositories;
using HotelManagement.Infrastructure.Paging;

namespace HotelManagement.Application.Services
{
    public class HotelRoomService : IHotelRoomService
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;
        private readonly IAppLogger<HotelRoomService> _logger;

        public HotelRoomService(IHotelRoomRepository hotelRoomRepository, IAppLogger<HotelRoomService> logger)
        {
            _hotelRoomRepository = hotelRoomRepository ?? throw new ArgumentNullException(nameof(hotelRoomRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<HotelRoomModel>> GetCheapestRoomPrices()
        {
            var hotelRoomList = await _hotelRoomRepository.GetCheapestRooms();

            var hotelRoomModels = ObjectMapper.Mapper.Map<IEnumerable<HotelRoomModel>>(hotelRoomList);

            return hotelRoomModels;
        }

        public async Task<IPagedList<HotelRoomModel>> SearchHotelRooms(PageSearchArgs args)
        {
            var hotelRoomPagedList = await _hotelRoomRepository.SearchHotelRooms(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var hotelRoomModels = ObjectMapper.Mapper.Map<List<HotelRoomModel>>(hotelRoomPagedList.Items);

            var hotelRoomModelPagedList = new PagedList<HotelRoomModel>(
                hotelRoomPagedList.PageIndex,
                hotelRoomPagedList.PageSize,
                hotelRoomPagedList.TotalCount,
                hotelRoomPagedList.TotalPages,
                hotelRoomModels);

            return hotelRoomModelPagedList;
        }

        public async Task<IEnumerable<HotelRoomAvailabilityModel>> RoomAvailabilityCheck(AvailabilitySearchArgs args)
        {
            var hotelRoomAvailabilities = await _hotelRoomRepository.RoomAvailabilityCheck(args.HotelIds, args.RoomTypeIds, args.RequestedRoomCount);

            var hotelRoomAvailabilityModels = hotelRoomAvailabilities.ToList().Select(availability => new HotelRoomAvailabilityModel
            {
                Hotel = ObjectMapper.Mapper.Map<HotelModel>(availability.Item1),
                Available = availability.Item2
            });

            return hotelRoomAvailabilityModels;
        }
    }
}