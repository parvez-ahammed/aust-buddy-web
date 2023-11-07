using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
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

			
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "1082440138378-ilf868uelbccv425d0b22j16fbhmu27b.apps.googleusercontent.com",
                ClientSecret = "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",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,

            });
        }
	}
}