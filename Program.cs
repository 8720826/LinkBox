using Home.Migrator;
using LinkBox.Contexts;
using System.Text.Json.Serialization;

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
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            //builder.Services.AddUserAuthentication();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			builder.Services.AddScoped<IMigratorService, MigratorService>();
			//builder.Services.AutoRegister();
			builder.Services.AddHealthChecks();
            //builder.Services.ConfigureModelBindingExceptionHandling();
            builder.Services.AddDbContext<LinkboxDbContext>();
            builder.Services.AddMigrate();


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

            app.MapRazorPages();
            app.MapHealthChecks("/health");
            app.Run();
        }
    }
}
