using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lotify.Startup))]
namespace Lotify
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
