using HotelManagement.Core.Entities;
using HotelManagement.Core.Specifications.Base;

namespace HotelManagement.Core.Specifications
{
    public class HotelSpecification : BaseSpecification<Hotel>
    {
        public HotelSpecification(string hotelName)
            : base(p => p.Name.Contains(hotelName))
        {
        }

        public HotelSpecification(int hotelId)
            : base(p => p.Id == hotelId)
        {
        }

        public HotelSpecification()
            : base(null)
        {
        }
    }
}