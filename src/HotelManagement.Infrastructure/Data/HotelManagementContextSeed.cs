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
        private readonly IRepository<HotelRoom> _hotelRoomRepository;
        private readonly IRepository<Reservation> _reservationRepository;

        public HotelManagementContextSeed(
            HotelManagementContext hotelManagementContext,
            UserManager<HotelManagementUser> userManager,
            IHotelRepository hotelRepository,
            IRepository<HotelRoom> hotelRoomRepository,
            IRepository<Reservation> reservationRepository)
        {
            _hotelManagementContext = hotelManagementContext;
            _userManager = userManager;
            _hotelRepository = hotelRepository;
            _hotelRoomRepository = hotelRoomRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task SeedAsync()
        {
            // TODO: Only run this if using a real database
            // _hotelManagementContext.Database.Migrate();
            // _hotelManagementContext.Database.EnsureCreated();

            // users
            await SeedUsersAsync();
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