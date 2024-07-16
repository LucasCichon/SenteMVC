using Sente.Application.Interfaces;
using Sente.Application.Mapping;
using Sente.Application.Services;
using Sente.Domain.Interfaces;
using Sente.Infrastructure.Repositories;
using System.Reflection;

namespace SenteMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IConfigurationService, ConfigurationService>();
            builder.Services.AddAutoMapper(typeof(WorklogMappingProfile));
            builder.Services.AddTransient<IWorklogRepository, WorklogRepository>();
            builder.Services.AddTransient<IWorklogService, WorklogService>();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            builder.Services.AddSingleton(configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Worklog}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
