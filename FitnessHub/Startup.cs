using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FitnessHub.Startup))]
namespace FitnessHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
