using System.Web.Mvc;
namespace AUST_BUDDY_WEB.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}



		public ActionResult Announcement()
		{
			ViewBag.Message = "Your annoncement page.";
			return View();
		}
		public ActionResult Group()
		{
			ViewBag.Message = "Your Group page.";
			return View();
		}

		public ActionResult Contribute()
		{
			ViewBag.Message = "Your Contribute page.";
			return View();
		}

		public ActionResult AddCourse()
		{
			ViewBag.Message = "Your add course page.";
			return View();
		}

		public ActionResult AddTeacher()
		{
			ViewBag.Message = "Your add course page.";
			return View();
		}

		public ActionResult Announcements()
		{
			ViewBag.Message = "Your announcement page.";
			return View();
		}
		public ActionResult AddAnnouncement()
		{
			ViewBag.Message = "Your add announcement page.";
			return View();
		}

		public ActionResult Acccount()
		{
			ViewBag.Message = "Your account page.";
			return View();
		}
		public ActionResult AdminPanel()
		{
			ViewBag.Message = "Your admin panel page.";
			return View();
		}



	}
}