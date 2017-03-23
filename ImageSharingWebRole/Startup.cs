using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImageSharingWebRole.Startup))]
namespace ImageSharingWebRole
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
