using AUST_BUDDY_WEB.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AUST_BUDDY_WEB.Controllers
{
	public class AnnouncementController : Controller
	{
		private readonly IFirebaseConfig _firebaseConfig;
		private readonly IFirebaseClient _firebaseClient;
		// GET: Announcement
		public ActionResult Index()
		{
			List<Announcement> announcementList;
			string path = "public-announcements/all";
			announcementList = GetCAnnouncementDataFromFirebase(path);
			return View(announcementList);
		}
		public AnnouncementController()
		{
			_firebaseConfig = new FirebaseConfig
			{
				AuthSecret = "fbs0BK9VcSR6Y2xZ6CSjA0JIK1S8yBCaVFQ00aXK",
				BasePath = "https://aust-buddies-default-rtdb.asia-southeast1.firebasedatabase.app"
			};
			_firebaseClient = new FireSharp.FirebaseClient(_firebaseConfig);
		}

		private List<Announcement> GetCAnnouncementDataFromFirebase(string path)
		{
			// Replace "courses" with the path to your Firebase collection where course data is stored.
			FirebaseResponse response = _firebaseClient.Get(path);

			// Check if the data is null or not found
			if (response.Body == "null")
			{
				return new List<Announcement>();
			}

			var announcement = response.ResultAs<Dictionary<string, Announcement>>();
			return announcement.Values.ToList();
		}

		public ActionResult Announcement()
		{

			List<Announcement> announcementList;
			string path = "public-announcements/all";
			announcementList = GetCAnnouncementDataFromFirebase(path);
			return View(announcementList);

		}
		public ActionResult Add()
		{
			return View();
		}
	}
}