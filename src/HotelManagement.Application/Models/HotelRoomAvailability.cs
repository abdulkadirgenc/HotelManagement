using HotelManagement.Application.Models.Entity;

namespace HotelManagement.Application.Models
{
    public class AvailabilitySearchArgs
    {
        public List<int> HotelIds { get; set; }
        public List<int> RoomTypeIds { get; set; }
        public int RequestedRoomCount { get; set; }
    }

    public class HotelRoomAvailabilityModel
    {
        public HotelModel Hotel { get; set; }
        public bool Available { get; set; }
    }
}
