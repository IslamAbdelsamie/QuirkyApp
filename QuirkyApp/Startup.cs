using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuirkyApp.Startup))]
namespace QuirkyApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
