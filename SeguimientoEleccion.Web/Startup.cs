using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SeguimientoEleccion.Web.Startup))]
namespace SeguimientoEleccion.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
