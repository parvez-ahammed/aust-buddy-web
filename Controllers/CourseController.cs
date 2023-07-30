using AUST_BUDDY_WEB.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AUST_BUDDY_WEB.Controllers
{
	public class CourseController : Controller
	{
		// GET: Course
		private readonly IFirebaseConfig _firebaseConfig;
		private readonly IFirebaseClient _firebaseClient;
		public ActionResult Index()
		{
			FirebaseResponse response = _firebaseClient.Get("courses");
			var courses = response.ResultAs<Dictionary<string, Course>>();

			return View(courses.Values);
		}

		public CourseController()
		{
			_firebaseConfig = new FirebaseConfig
			{
				AuthSecret = "fbs0BK9VcSR6Y2xZ6CSjA0JIK1S8yBCaVFQ00aXK",
				BasePath = "https://aust-buddies-default-rtdb.asia-southeast1.firebasedatabase.app"
			};
			_firebaseClient = new FireSharp.FirebaseClient(_firebaseConfig);
		}
		// Method to retrieve data from Firebase and return a List<CourseModel>
		private List<Course> GetCourseDataFromFirebase()
		{
			// Replace "courses" with the path to your Firebase collection where course data is stored.
			FirebaseResponse response = _firebaseClient.Get("Courses");

			// Check if the data is null or not found
			if (response.Body == "null")
			{
				return new List<Course>();
			}

			var courses = response.ResultAs<Dictionary<string, Course>>();
			return courses.Values.ToList();
		}

		public ActionResult Course()
		{

			List<Course> courseDataList = GetCourseDataFromFirebase();


			return View(courseDataList);
		}

		public ActionResult Add()
		{
			return View();
		}
		public ActionResult Choose()
		{
			return View();
		}



	}
}