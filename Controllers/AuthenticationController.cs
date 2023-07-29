using System.Web.Mvc;

namespace AUST_BUDDY_WEB.Controllers
{
	public class AuthenticationController : Controller
	{
		// GET: Authentication
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Login()
		{
			ViewBag.Message = "Your login page.";
			return View();
		}

		public ActionResult Registration()
		{
			ViewBag.Message = "Your Registration page.";
			return View();
		}
	}
}