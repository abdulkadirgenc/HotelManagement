using HotelManagement.Core.Entities;
using HotelManagement.Core.Repositories;
using HotelManagement.Core.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Data
{
    public class HotelManagementContextSeed
    {
        private readonly HotelManagementContext _hotelManagementContext;
        private readonly UserManager<HotelManagementUser> _userManager;
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelRoomRepository _hotelRoomRepository;
        private readonly IRepository<RoomType> _roomTypeRepository;
        private readonly IReservationRepository _reservationRepository;

        public HotelManagementContextSeed(
            HotelManagementContext hotelManagementContext,
            UserManager<HotelManagementUser> userManager,
            IHotelRepository hotelRepository,
            IHotelRoomRepository hotelRoomRepository,
            IRepository<RoomType> roomTypeRepository,
            IReservationRepository reservationRepository)
        {
            _hotelManagementContext = hotelManagementContext;
            _userManager = userManager;
            _hotelRepository = hotelRepository;
            _hotelRoomRepository = hotelRoomRepository;
            _roomTypeRepository = roomTypeRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task SeedAsync()
        {
            //TODO: Only run this if using a real database
            //_hotelManagementContext.Database.Migrate();
            _hotelManagementContext.Database.EnsureCreated();

            // room types
            await SeedRoomTypesAsync();

            // hotels
            await SeedHotelsAsync();

            // hotel rooms
            await SeedHotelRoomsAsync();

            // users
            await SeedUsersAsync();
        }

        private async Task SeedRoomTypesAsync()
        {
            if (!_roomTypeRepository.Table.Any())
            {
                var roomTypes = new List<RoomType>
                {
                    new RoomType() { Name = "Standart"}, // 1
                    new RoomType() { Name = "Deluxe"}, // 2
                    new RoomType() { Name = "Suite"}, // 3
                    new RoomType() { Name = "Presidential"}, // 4
                    new RoomType() { Name = "Villa"}, // 5
                };

                await _roomTypeRepository.AddRangeAsync(roomTypes);
            }
        }

        private async Task SeedHotelsAsync()
        {
            if (!_hotelRepository.Table.Any())
            {
                var hotels = new List<Hotel>
                {
                    new Hotel() { Name = "Titanic Mardan Palaca" },
                    new Hotel() { Name = "Akra Hotel" },
                    new Hotel() { Name = "Hotel SU & Aqualand" },
                    new Hotel() { Name = "Rixos Downtown Antalya" },
                    new Hotel() { Name = "The Marmara Antalya" },
                    new Hotel() { Name = "Concorde De Luxe Resort" },
                    new Hotel() { Name = "Crowne Plaza Antalya" },
                    new Hotel() { Name = "Terrace Beach Resort" },
                };

                await _hotelRepository.AddRangeAsync(hotels);
            }
        }

        private async Task SeedHotelRoomsAsync()
        {
            if (!_hotelRoomRepository.Table.Any())
            {
                var hotelRooms = from hotel in _hotelRepository.Table
                                 from roomType in _roomTypeRepository.Table
                                 select new HotelRoom
                                 {
                                     Hotel = hotel,
                                     RoomType = roomType,
                                     Price = 300 + (Math.Abs(hotel.Name.GetHashCode() * roomType.Name.GetHashCode())) % 500,
                                     MaxAllotment = 50 + (Math.Abs(hotel.Name.GetHashCode() * roomType.Name.GetHashCode())) % 60,
                                     SoldAllotment = 0,
                                 };

                await _hotelRoomRepository.AddRangeAsync(hotelRooms);
            }
        }

        private async Task SeedUsersAsync()
        {
            var user = await _userManager.FindByEmailAsync("hotelmanagement@outlook.com");
            if (user == null)
            {
                user = new HotelManagementUser
                {
                    FirstName = "hotel",
                    LastName = "management",
                    Email = "hotelmanagement@outlook.com",
                    UserName = "hotelmanagement"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in Seeding");
                }

                _hotelManagementContext.Entry(user).State = EntityState.Unchanged;
            }
        }
    }
}