using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KEYSAFE.Startup))]
namespace KEYSAFE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
