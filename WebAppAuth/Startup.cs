using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAppAuth.Startup))]

namespace WebAppAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
