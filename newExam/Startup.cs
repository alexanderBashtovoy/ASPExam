using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(newExam.Startup))]
namespace newExam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
