using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Jobs;
using LinkBox.Mappings;
using LinkBox.Migrator;
using LinkBox.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using System.IO;
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

            // 注册映射配置
            MappingConfig.RegisterMappings();

            // Add services to the container.
            builder.Services.AddRazorPages().AddMvcOptions(options =>
            {
                options.MaxModelValidationErrors = 50;
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "请输入内容");
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => $"输入值'{x}'无效");
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => $"输入值'{x}'无效");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((x) => "只能输入数字");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((x) => $"缺少属性 '{x}'");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "请输入内容");
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "输入值无效");
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "RequestBody 不能为空");
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "只能输入数字");
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => $"输入值'{x}'无效");
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "输入值无效");
            });

            builder.Services.Configure<RouteOptions>(option =>
            {
                option.LowercaseUrls = true;
                option.LowercaseQueryStrings = true;
            });

            builder.Services.AddMemoryCache();
            builder.Services.AddUserAuthentication();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			builder.Services.AddScoped<IMigratorService, MigratorService>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddHealthChecks();
            
            var dir = Directory.GetCurrentDirectory();
            builder.Services.AddDbContext<LinkboxDbContext>();
            builder.Services.AddMigrate(dir);
            builder.Services.AddTemplate(dir);
            builder.Services.AddHostedService<TemplateJob>();
            builder.Services.AddHostedService<LinkJob>();
            
            // 注册应用服务
            builder.Services.AddApplicationServices();
            
            // 添加控制器
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = false;
                });

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseDefaultFiles(new DefaultFilesOptions() { DefaultFileNames = new List<string> { "index.html" } });
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age=600");
                }
            });
            
            app.UseAuthentication();
           
            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.MapHealthChecks("/health");
            app.Run();
        }
    }
}
