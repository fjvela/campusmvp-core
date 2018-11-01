using Calculator.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class CustomErrorPagesExtensions
    {
        public static IApplicationBuilder UseCustomErrorPages(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomErrorPagesMiddleware>();
        }
    }
}