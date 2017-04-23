using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyGradesAvgProject.Startup))]
namespace MyGradesAvgProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
