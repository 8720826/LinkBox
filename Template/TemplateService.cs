using Microsoft.Extensions.Hosting;

namespace LinkBox.Template
{
    public class TemplateService: ITemplateService
    {
        public readonly IHostEnvironment _hostEnvironment;
        public TemplateService(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;

        }
            



        public string Reset(string file)
        {
            var defaultpath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "template", file);
            var html = System.IO.File.ReadAllText(defaultpath, System.Text.Encoding.UTF8);

            var newpath = Path.Combine(_hostEnvironment.ContentRootPath, "data", "template", file);
            System.IO.File.WriteAllText(newpath, html);

            return html;
        }

        public void Update(string file, string content)
        {
            var newpath = Path.Combine(_hostEnvironment.ContentRootPath, "data", "template", file);
            System.IO.File.WriteAllText(newpath, content);
        }

        public string Read(string file)
        {
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "data", "template", file);
            return System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
        }
    }
}
