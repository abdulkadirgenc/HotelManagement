namespace HotelManagement.Application.Models
{
    public class CreateReservationRequest
    {
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int RequestedRoomCount { get; set; }
        public DateTime BookingDateStart { get; set; }
        public DateTime BookingDateEnd { get; set; }
    }

    public class CreateReservationResponse
    {
        public int? ReservationId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}