using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nunana.Startup))]
namespace Nunana
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
