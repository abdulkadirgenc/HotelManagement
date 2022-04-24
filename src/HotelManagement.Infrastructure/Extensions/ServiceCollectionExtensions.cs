using FluentValidation.AspNetCore;
using HotelManagement.Core.Configuration;
using HotelManagement.Core.Entities;
using HotelManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Hosting;
using HotelManagement.Infrastructure.Misc;
using HotelManagement.Infrastructure.IoC;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Builder;

namespace HotelManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this WebApplicationBuilder builder)
        {
            var hotelManagementSettings = builder.Configuration.Get<HotelManagementSettings>();

            builder.Services
                .AddCustomController()
                .AddCustomDbContext(hotelManagementSettings)
                .AddCustomIdentity()
                .AddCustomSwagger()
                .AddCustomConfiguration(builder.Configuration)
                .AddCustomAuthentication(hotelManagementSettings)
                .AddCustomIntegrations(builder.Environment, builder.Host);
        }

        public static IServiceCollection AddCustomController(this IServiceCollection services)
        {
            // Add framework services.
            services
                .AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.DisableDataAnnotationsValidation = true;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, HotelManagementSettings hotelManagementSettings)
        {
            // use in-memory database
            //services.AddDbContext<HotelManagementContext>(c => c.UseInMemoryDatabase("HotelManagement"));

            // Add HotelManagement DbContext
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<HotelManagementContext>(options =>
                        options.UseSqlServer(hotelManagementSettings.ConnectionString,
                        sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        }
                    ),
                    ServiceLifetime.Scoped
                 );

            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var existingUserManager = scope.ServiceProvider.GetService<UserManager<HotelManagementUser>>();

                if (existingUserManager == null)
                {
                    services.AddIdentity<HotelManagementUser, HotelManagementRole>(
                        cfg =>
                        {
                            cfg.User.RequireUniqueEmail = true;
                        })
                        .AddEntityFrameworkStores<HotelManagementContext>()
                        .AddDefaultTokenProviders();
                }
            }

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "HotelManagement HTTP API",
                    Description = "The HotelManagement Service HTTP API",
                    Contact = new OpenApiContact
                    {
                        Name = "HotelManagement",
                        Email = string.Empty,
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<HotelManagementSettings>(configuration);

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, HotelManagementSettings hotelManagementSettings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      ValidIssuer = hotelManagementSettings.Tokens.Issuer,
                      ValidAudience = hotelManagementSettings.Tokens.Audience,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(hotelManagementSettings.Tokens.Key))
                  };
              });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IHostEnvironment hostEnvironment, ConfigureHostBuilder host)
        {
            services.AddHttpContextAccessor();

            //configure autofac
            host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                var fileProvider = new AppFileProvider(hostEnvironment);
                var typeFinder = new WebAppTypeFinder(fileProvider);

                //register type finder
                containerBuilder.RegisterInstance(fileProvider).As<IAppFileProvider>().SingleInstance();
                containerBuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

                //find dependency registrars provided by other assemblies
                var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();

                //create and sort instances of dependency registrars
                var instances = dependencyRegistrars
                    .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                    .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

                //register all provided dependencies
                foreach (var dependencyRegistrar in instances)
                    dependencyRegistrar.Register(containerBuilder, typeFinder);
            });

            return services;
        }
    }
}
