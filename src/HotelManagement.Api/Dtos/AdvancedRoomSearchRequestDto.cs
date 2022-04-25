namespace HotelManagement.Api.Requests
{
    public class AdvancedRoomSearchRequestDto
    {
        public List<int> SelectedHotelIds { get; set; }
        public List<int> RoomTypeIds { get; set; }
    }
}
