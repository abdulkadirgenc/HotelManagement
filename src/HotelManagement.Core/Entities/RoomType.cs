using HotelManagement.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Entities;

public class RoomType : Entity
{
    [Required, StringLength(20)]
    public string Name { get; set; }
}
