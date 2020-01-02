using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MieszalnikUAA.Startup))]
namespace MieszalnikUAA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
