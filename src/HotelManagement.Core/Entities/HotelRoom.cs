using HotelManagement.Core.Entities.Base;

namespace HotelManagement.Core.Entities
{
    public class HotelRoom : Entity
    {
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }
        public int MaxAllotment { get; set; }
        public int SoldAllotment { get; set; }
    }
}