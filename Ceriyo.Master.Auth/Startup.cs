using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Ceriyo.Master.Auth.Startup))]

namespace Ceriyo.Master.Auth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
