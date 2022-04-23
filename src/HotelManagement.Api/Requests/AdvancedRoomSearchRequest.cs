namespace HotelManagement.Api.Requests
{
    public class AdvancedRoomSearchRequest
    {
        public List<int> SelectedHotelIds { get; set; }
        public List<int> RoomTypeIds { get; set; }
    }
}
