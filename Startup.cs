using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentOrganization.Startup))]
namespace StudentOrganization
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
