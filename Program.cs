using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Migrator;
using LinkBox.Template;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace LinkBox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.Configure<RouteOptions>(option =>
            {
                option.LowercaseUrls = true;
                option.LowercaseQueryStrings = true;
            });

            builder.Services.AddMemoryCache();
            builder.Services.AddUserAuthentication();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			builder.Services.AddScoped<IMigratorService, MigratorService>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<ITemplateService, TemplateService>();
            //builder.Services.AutoRegister();
            builder.Services.AddHealthChecks();
            //builder.Services.ConfigureModelBindingExceptionHandling();

            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "data", "linkbox.db");
            builder.Services.AddDbContext<LinkboxDbContext>();
            builder.Services.AddMigrate(dbPath);
            builder.Services.AddTemplate();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseGlobalExceptionMiddleware();
            app.UseAuthentication();
           
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapTemplate("/");
            });

            app.MapRazorPages();
            app.MapHealthChecks("/health");
            app.Run();
        }
    }
}
