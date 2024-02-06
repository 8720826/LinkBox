﻿
using Microsoft.Extensions.Hosting;

namespace LinkBox.Template
{
    public class TemplateJob : BackgroundService
    {
        private ILogger<TemplateJob> _logger;
        public readonly IHostEnvironment _hostEnvironment;

        public TemplateJob(ILogger<TemplateJob> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // 执行的方法
                    CompileHtml();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Job {nameof(TemplateJob)} threw an exception!");
                }
                // 间隔的时间
                await Task.Delay(1000);
            }
           
        }

        private void CompileHtml()
        {
            if (DateTime.Now.Subtract(TemplateProvider.NextCompileTime).TotalSeconds > 0)
            {
                try
                {
                    var tpl = TemplateProvider.Read(_hostEnvironment.ContentRootPath, "index.tpl");
                    var css = TemplateProvider.Read(_hostEnvironment.ContentRootPath, "index.css");
                    var js = TemplateProvider.Read(_hostEnvironment.ContentRootPath, "index.js");

                    var result = TemplateProvider.Compile(tpl, css, js);

                    TemplateProvider.Update(_hostEnvironment.ContentRootPath, "index.html", result);

                    TemplateProvider.NextCompileTime = DateTime.Now.AddMinutes(5);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Job {nameof(TemplateJob)} threw an exception!");
                }
            }
        }
    }
}
