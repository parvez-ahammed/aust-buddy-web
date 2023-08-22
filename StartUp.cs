using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(AUST_BUDDY_WEB.App_Start.StartUp))]

namespace AUST_BUDDY_WEB
{
	public partial class StartUp
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/authentication/login"), // Set your login page path
				LogoutPath = new PathString("/authentication/logout"),
				// Additional configuration options...
			});

		}

	}
}
