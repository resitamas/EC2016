using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EC2016.Startup))]
namespace EC2016
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
