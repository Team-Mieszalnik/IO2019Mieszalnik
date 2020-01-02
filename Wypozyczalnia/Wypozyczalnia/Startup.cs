using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wypozyczalnia.Startup))]
namespace Wypozyczalnia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
