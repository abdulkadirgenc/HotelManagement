using HotelManagement.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Core.Entities
{
    public class HotelManagementRole : IdentityRole<int>, IEntityBase<int>
    {
    }
}