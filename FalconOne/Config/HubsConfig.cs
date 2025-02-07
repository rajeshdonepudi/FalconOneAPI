using FalconeOne.BLL.Hubs.Dashboards;
using FalconeOne.BLL.Hubs.Meeting;
using FalconOne.Security;

namespace FalconOne.API.Config
{
    public static class HubsConfig
    {
        public static void Configure(WebApplication app)
        {
            app.MapHub<UserDashboardHub>("hubs/user-dashboard").RequireCors(CORSPolicy.ReactAppPolicy);
            app.MapHub<MeetingHubBasic>("hubs/meeting/pro").RequireCors(CORSPolicy.ReactAppPolicy);
        }
    }
}
