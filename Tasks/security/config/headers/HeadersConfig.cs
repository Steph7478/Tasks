namespace Tasks.Security.Config.Headers
{
    public static class SecurityHeaders
    {
        public static void ApplySecurityHeaders(this IApplicationBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Append("Content-Security-Policy",
                "default-src 'self'; " +
                "style-src 'self' 'unsafe-inline'; " +
                "script-src 'self'; " +
                "img-src 'self' data:; " +
                "font-src 'self'; " +
                "frame-ancestors 'none';"
                );

                ctx.Response.Headers.Append("Referrer-Policy", "no-referrer");
                ctx.Response.Headers.Append("X-Frame-Options", "DENY");
                await next();
            });
        }
    }
}
