using HotelManagement.Core.Entities;
using HotelManagement.Core.Specifications.Base;

namespace HotelManagement.Core.Specifications
{
    public class HotelSpecification : BaseSpecification<Hotel>
    {
        public HotelSpecification(string hotelName)
            : base(h => h.Name.Contains(hotelName))
        {
        }

        public HotelSpecification(int hotelId)
            : base(h => h.Id == hotelId)
        {
        }

        public HotelSpecification()
            : base(null)
        {
        }
    }
}