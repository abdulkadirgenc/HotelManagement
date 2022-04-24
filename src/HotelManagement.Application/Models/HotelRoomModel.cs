using HotelManagement.Application.Models.Base;

namespace HotelManagement.Application.Models
{
    public class HotelRoomModel : BaseModel
    {
        public HotelModel Hotel { get; set; }
        public RoomTypeModel RoomType { get; set; }
        public decimal Price { get; set; }
        public int MaxAllotment { get; set; }
        public int SoldAllotment { get; set; }
    }
}