using HotelManagement.Application.Mapper;
using HotelManagement.Application.Models;
using HotelManagement.Core.Entities;
using HotelManagement.Core.Interfaces;
using HotelManagement.Core.Paging;
using HotelManagement.Core.Repositories;
using HotelManagement.Infrastructure.Paging;
using HotelManagement.Application.Interfaces;
using HotelManagement.Core.Specifications;

namespace HotelManagement.Application.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IAppLogger<HotelService> _logger;

    public HotelService(IHotelRepository hotelRepository, IAppLogger<HotelService> logger)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<HotelModel>> GetHotelList()
    {
        var hotelList = await _hotelRepository.ListAllAsync();

        var hotelModels = ObjectMapper.Mapper.Map<IEnumerable<HotelModel>>(hotelList);

        return hotelModels;
    }

    public async Task<IPagedList<HotelModel>> SearchHotels(PageSearchArgs args)
    {
        var hotelPagedList = await _hotelRepository.SearchHotelsAsync(args);

        //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        var hotelModels = ObjectMapper.Mapper.Map<List<HotelModel>>(hotelPagedList.Items);

        var hotelModelPagedList = new PagedList<HotelModel>(
            hotelPagedList.PageIndex,
            hotelPagedList.PageSize,
            hotelPagedList.TotalCount,
            hotelPagedList.TotalPages,
            hotelModels);

        return hotelModelPagedList;
    }

    public async Task<HotelModel> GetHotelById(int hotelId)
    {
        var hotel = await _hotelRepository.GetByIdAsync(hotelId);

        var hotelModel = ObjectMapper.Mapper.Map<HotelModel>(hotel);

        return hotelModel;
    }

    public async Task<IEnumerable<HotelModel>> GetHotelsByName(string name)
    {
        var spec = new HotelSpecification(name);
        var hotelList = await _hotelRepository.GetAsync(spec);

        var hotelModels = ObjectMapper.Mapper.Map<IEnumerable<HotelModel>>(hotelList);

        return hotelModels;
    }

    public async Task<HotelModel> CreateHotel(HotelModel hotel)
    {
        var existingHotel = await _hotelRepository.GetByIdAsync(hotel.Id);
        if (existingHotel != null)
        {
            throw new ApplicationException("Hotel with this id already exists");
        }

        var newHotel = ObjectMapper.Mapper.Map<Hotel>(hotel);
        newHotel = await _hotelRepository.SaveAsync(newHotel);

        _logger.LogInformation("Entity successfully added - HotelManagementAppService");

        var newHotelModel = ObjectMapper.Mapper.Map<HotelModel>(newHotel);
        return newHotelModel;
    }

    public async Task UpdateHotel(HotelModel hotel)
    {
        var existingHotel = await _hotelRepository.GetByIdAsync(hotel.Id);
        if (existingHotel == null)
        {
            throw new ApplicationException("Hotel with this id is not exists");
        }

        existingHotel.Name = hotel.Name;

        await _hotelRepository.SaveAsync(existingHotel);

        _logger.LogInformation("Entity successfully updated - HotelManagementAppService");
    }

    public async Task DeleteHotelById(int hotelId)
    {
        var existingHotel = await _hotelRepository.GetByIdAsync(hotelId);
        if (existingHotel == null)
        {
            throw new ApplicationException("Hotel with this id is not exists");
        }

        await _hotelRepository.DeleteAsync(existingHotel);

        _logger.LogInformation("Entity successfully deleted - HotelManagementAppService");
    }
}
