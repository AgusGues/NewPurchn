using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GRCweb1.Startup))]
namespace GRCweb1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
