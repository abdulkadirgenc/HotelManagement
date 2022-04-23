using HotelManagement.Api.Application.Commands;
using HotelManagement.Api.Application.Validations;
using HotelManagement.Infrastructure.IoC;
using HotelManagement.Infrastructure.Misc;
using Autofac;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace HotelManagement.Api.IoC
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateHotelCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder.RegisterAssemblyTypes(typeof(CreateHotelRequestValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
        }

        public int Order => 0;
    }
}