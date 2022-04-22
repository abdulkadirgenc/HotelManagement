using HotelManagement.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Entities;

public class Hotel : Entity
{
    [Required, StringLength(80)]
    public string Name { get; set; }

    public List<HotelRoom> Rooms { get; set; } = new List<HotelRoom>();
}
