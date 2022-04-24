using HotelManagement.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace HotelManagement.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<LoggingMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseRouting();
        }
    }
}