using HotelManagement.Application.Models.Entity;
using HotelManagement.Core.Entities;
using AutoMapper;

namespace HotelManagement.Application.Mapper
{
    public class ObjectMapper
    {
        public static IMapper Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Hotel, HotelModel>().ReverseMap();
                cfg.CreateMap<HotelRoom, HotelRoomModel>().ReverseMap();
                cfg.CreateMap<RoomType, RoomTypeModel>().ReverseMap();
                cfg.CreateMap<Reservation, ReservationModel>().ReverseMap();
            }).CreateMapper();
    }
}