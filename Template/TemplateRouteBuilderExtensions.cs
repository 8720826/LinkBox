

namespace LinkBox.Template
{
    public static class TemplateRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapTemplate(this IEndpointRouteBuilder endpoints,string pattern)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            var pipeline = endpoints.CreateApplicationBuilder()
               .UseMiddleware<TemplateMiddleware>()
               .Build();

            return endpoints.Map(pattern, pipeline);
        }
    }
}
