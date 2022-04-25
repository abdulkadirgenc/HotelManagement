using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;
using HotelManagement.Core.Entities;
using HotelManagement.Core.Interfaces;
using HotelManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IHotelRoomRepository _hotelRoomRepository;
        private readonly IAppLogger<ReservationService> _logger;

        public ReservationService(
            IReservationRepository reservationRepository,
            IHotelRoomRepository hotelRoomRepository,
            IAppLogger<ReservationService> logger
            )
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _hotelRoomRepository = hotelRoomRepository ?? throw new ArgumentNullException(nameof(hotelRoomRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateReservationResponse> CreateReservation(CreateReservationRequest createReservation)
        {
            var hotelRoom = await _hotelRoomRepository.Table
                .Where(hr => hr.HotelId == createReservation.HotelId && createReservation.RoomTypeId == hr.RoomTypeId)
                .SingleOrDefaultAsync();

            if (hotelRoom == null)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    Message = "Hotel Not Found"
                };
            }

            if (hotelRoom.MaxAllotment - hotelRoom.SoldAllotment < createReservation.RequestedRoomCount)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    Message = "Not enough room for reservation"
                };
            }

            //TODO: For data consistency, the record should be updated with SQL query.
            //The new value of the Sold Allotment field should be checked when updating, that it is not greater than Max Allotment.
            //The following query should be used.
            //UPDATE HotelRoom SET SoldAllotment = SoldAllotment + :RequestedRoomCount WHERE Id = :HotelId AND SoldAllotment + :RequestedRoomCount <= MaxAllotment
            hotelRoom.SoldAllotment += createReservation.RequestedRoomCount;

            await _hotelRoomRepository.SaveAsync(hotelRoom);

            var newReservation = new Reservation
            {
                HotelRoomId = hotelRoom.Id,
                StartDate = createReservation.BookingDateStart,
                EndDate = createReservation.BookingDateEnd,
                RoomCount = createReservation.RequestedRoomCount
            };

            newReservation = await _reservationRepository.SaveAsync(newReservation);

            //var newReservationModel = ObjectMapper.Mapper.Map<ReservationModel>(newReservation);
            return new CreateReservationResponse
            {
                //Reservation = newReservation,
                ReservationId = newReservation.Id,
                Success = true,
                Message = "Successfully booked"
            };
        }

        public async Task<CancelReservationResponse> CancelReservation(int reservationId)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (existingReservation == null)
            {
                return new CancelReservationResponse
                {
                    Success = false,
                    Message = "Reservation with this id is not exists"
                };
                //throw new ApplicationException("Reservation with this id is not exists");
            }

            // Delete reservation
            await _reservationRepository.DeleteAsync(existingReservation);

            // Update hotel room
            var hotelRoom = await _hotelRoomRepository.GetByIdAsync(existingReservation.HotelRoomId);

            hotelRoom.SoldAllotment -= existingReservation.RoomCount;

            await _hotelRoomRepository.SaveAsync(hotelRoom);


            return new CancelReservationResponse
            {
                Success = true,
                Message = "Reservation with this id is not exists"
            };
        }
    }
}