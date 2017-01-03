using Owin;

namespace StructuredLogging.WebApi
{
    public partial class Startup
    {
        public void ConfigureSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}