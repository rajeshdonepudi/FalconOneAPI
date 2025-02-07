using FalconOne.Security;

namespace FalconOne.API.Config
{
    public static class CORSConfig
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CORSPolicy.ReactAppPolicy, (p) =>
                {
                    p.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("https://falconone-ui.web.app",
                                 "http://falconone-ui.web.app",
                                 "http://localhost:5173",
                                 "https://localhost:5173");
                });
            });
        }
    }
}
