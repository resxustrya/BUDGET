using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BUDGET.Startup))]
namespace BUDGET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
