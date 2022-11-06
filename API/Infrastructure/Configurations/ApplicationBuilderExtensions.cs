using Microsoft.AspNetCore.Builder;

namespace API.Infrastructure.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder app)
            => app.UseMiddleware<GlobalExceptionHandler>();
    }
}
