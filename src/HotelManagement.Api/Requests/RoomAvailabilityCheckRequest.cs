namespace HotelManagement.Api.Requests
{
    public class RoomAvailabilityCheckRequest
    {
        public List<int> HotelIds { get; set; }
        public List<int> RoomTypeIds { get; set; }
        public int RequestedRoomCount { get; set; }
    }
}
