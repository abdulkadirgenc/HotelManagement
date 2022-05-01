using HotelManagement.Core.Interfaces;
using HotelManagement.Core.Repositories;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IAppLogger<HotelService> _logger;

        public HotelService(IHotelRepository hotelRepository, IAppLogger<HotelService> logger)
        {
            _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
