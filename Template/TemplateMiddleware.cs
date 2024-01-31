using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using LinkBox.Models;
using System.Web;
using System.Diagnostics;
using RazorEngineCore;


namespace LinkBox.Template
{
    public class TemplateMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly IHostEnvironment _hostEnvironment;
        public TemplateMiddleware(RequestDelegate next, IHostEnvironment hostEnvironment)
        {
            _next = next;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "Template.cshtml");
            var html = File.ReadAllText(path,System.Text.Encoding.UTF8);
            
            LinkBoxData.Refresh();
            var appCategories = LinkBoxData.Categories.Where(x => x.Type == Entities.Enums.CategoryTypeEnum.App).Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                Links = LinkBoxData.Links.Where(y => y.CategoryId == x.Id).Select(y => new LinkModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Icon = y.Icon,
                    Title = y.Title,
                    Url = y.Url
                }).ToList()
            }).ToList();

            var bookmarkCategories = LinkBoxData.Categories.Where(x => x.Type == Entities.Enums.CategoryTypeEnum.BookMark).Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                Links = LinkBoxData.Links.Where(y => y.CategoryId == x.Id).Select(y => new LinkModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Icon = y.Icon,
                    Title = y.Title,
                    Url = y.Url
                }).ToList()
            }).ToList();
            var model = new TemplateModel { AppCategories = appCategories, BookmarkCategories = bookmarkCategories, Config = LinkBoxData.Config };

            Console.WriteLine($"time = {stopwatch.ElapsedMilliseconds}");

            var razorEngine = new RazorEngine();
            var template = razorEngine.Compile(html);

            var result = template.Run(model);

            Console.WriteLine($"time = {stopwatch.ElapsedMilliseconds}");

            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync(result, System.Text.Encoding.UTF8);

            Console.WriteLine($"time = {stopwatch.ElapsedMilliseconds}");
            stopwatch.Stop();
        }


    }
}
