using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(deneme.Startup))]
namespace deneme
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
