using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

namespace AUST_BUDDY_WEB.App_Start
{
	public partial class StartUp
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Authentication/Login"),
				LogoutPath = new PathString("/Authentication/Logout"),
				ExpireTimeSpan = TimeSpan.FromMinutes(30.0)
			});
		}
	}
}