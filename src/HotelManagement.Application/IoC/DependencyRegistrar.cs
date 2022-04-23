using HotelManagement.Application.Services;
using HotelManagement.Infrastructure.IoC;
using HotelManagement.Infrastructure.Misc;
using Autofac;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.IoC
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            // services
            builder.RegisterType<HotelService>().As<IHotelService>().InstancePerLifetimeScope();
            builder.RegisterType<HotelRoomService>().As<IHotelRoomService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}