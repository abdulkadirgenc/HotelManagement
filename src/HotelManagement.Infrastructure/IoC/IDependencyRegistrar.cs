using HotelManagement.Infrastructure.Misc;
using Autofac;

namespace HotelManagement.Infrastructure.IoC
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}