using HotelManagement.Core.Entities.Base;

namespace HotelManagement.Core.Entities
{
    public class Reservation : Entity
    {
        public int HotelRoomId { get; set; }
        public HotelRoom HotelRoom { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomCount { get; set; }
    }
}