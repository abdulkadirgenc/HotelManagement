using HotelManagement.Core.Entities;
using HotelManagement.Core.Specifications.Base;

namespace HotelManagement.Core.Specifications
{
    public class HotelRoomWithHotelAndTypeSpecification : BaseSpecification<HotelRoom>
    {
        public HotelRoomWithHotelAndTypeSpecification(int hotelRoomId)
            : base(hr => hr.Id == hotelRoomId)
        {
            AddInclude(hr => hr.Hotel);
            AddInclude(hr => hr.RoomType);
        }

        public HotelRoomWithHotelAndTypeSpecification()
            : base(null)
        {
            AddInclude(hr => hr.Hotel);
            AddInclude(hr => hr.RoomType);
        }
    }
}
