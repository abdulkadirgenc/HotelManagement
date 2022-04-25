using HotelManagement.Application.Models.Entity;
using MediatR;

namespace HotelManagement.Api.Requests
{
    public class UpdateHotelRequestDto : IRequest
    {
        public HotelModel Hotel { get; set; }
    }
}