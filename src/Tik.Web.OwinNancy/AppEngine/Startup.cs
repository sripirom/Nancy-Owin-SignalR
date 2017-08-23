using Owin;

namespace Tik.Web.OwinNancy.AppEngine
{
    public class Startup
    {
		public void Configuration(IAppBuilder app)
		{
			app.UseWelcomePage("/owin");
			app.UseNancy();
		}
	}
}
