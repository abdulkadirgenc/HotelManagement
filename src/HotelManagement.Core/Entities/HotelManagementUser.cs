using HotelManagement.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Core.Entities;

public class HotelManagementUser : IdentityUser<int>, IEntityBase<int>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime LastLoginTime { get; set; }

    public bool IsWorking { get; set; }
}
