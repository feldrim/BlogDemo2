using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogDemo2.Startup))]
namespace BlogDemo2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
