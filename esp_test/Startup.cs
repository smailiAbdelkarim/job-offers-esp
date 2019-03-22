using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(esp_test.Startup))]
namespace esp_test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
