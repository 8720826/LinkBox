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
            
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "data", "template/index.html");
            var html = File.ReadAllText(path,System.Text.Encoding.UTF8);

            var result = TemplateProvider.Compile(html);

           

            Console.WriteLine($"time = {stopwatch.ElapsedMilliseconds}");

            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync(result, System.Text.Encoding.UTF8);

            Console.WriteLine($"time = {stopwatch.ElapsedMilliseconds}");
            stopwatch.Stop();
        }


    }
}
