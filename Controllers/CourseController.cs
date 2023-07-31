using AUST_BUDDY_WEB.Features.Course;
using AUST_BUDDY_WEB.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
			FirebaseResponse response = _firebaseClient.Get("course-list/CSE/year1semester1");
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
		private List<Course> GetCourseDataFromFirebase(string path)
		{
			// Replace "courses" with the path to your Firebase collection where course data is stored.
			FirebaseResponse response = _firebaseClient.Get(path);

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
			return View();
		}
		[HttpGet]
		public ActionResult AddPlaylist()
		{
			return View();
		}

		public ActionResult Add()
		{
			return View();
		}
		public ActionResult Choose(int semester)
		{
			List<Course> courseDataList;
			string path = CourseUtility.GetYearSemester(semester);
			courseDataList = GetCourseDataFromFirebase(path);
			return View(courseDataList);
		}

		private readonly string playlistId = "PLom9DLdqyyk7r98iyUR-QAm6OeMijt1ud";
		public ActionResult Watch()
		{
			// YouTube Data API endpoint
			string apiUrl = $"https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId={playlistId}&key={credentials.youTubeApi}";

			// Make a request to the YouTube Data API
			HttpClient client = new HttpClient();
			HttpResponseMessage response = client.GetAsync(apiUrl).Result;
			string jsonResult = response.Content.ReadAsStringAsync().Result;

			// Parse the JSON response and extract video information
			JObject jsonObject = JObject.Parse(jsonResult);
			JArray items = (JArray)jsonObject["items"];

			// Create a model to hold video information
			var videoModel = new VideoModel();

			// Loop through the items and add video titles and embeds to the model
			foreach (var item in items)
			{
				string videoTitle = item["snippet"]["title"].ToString();
				string videoId = item["snippet"]["resourceId"]["videoId"].ToString();
				string videoEmbed = $"<iframe width='560' height='315' src='https://www.youtube.com/embed/{videoId}' frameborder='0' allowfullscreen></iframe>";

				videoModel.VideoTitles.Add(videoTitle);
				videoModel.VideoEmbeds.Add(videoEmbed);
			}
			return View(videoModel);
		}



		// ... (other methods)

		[HttpPost]
		public async Task<ActionResult> ScrapPlaylist(string playlistId)
		{
			try
			{
				// YouTube Data API endpoint
				string apiUrl = $"https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId={playlistId}&key={credentials.youTubeApi}";

				// Make a request to the YouTube Data API
				using (HttpClient client = new HttpClient())
				{
					HttpResponseMessage response = await client.GetAsync(apiUrl);
					string jsonResult = await response.Content.ReadAsStringAsync();

					// Parse the JSON response and extract video information
					JObject jsonObject = JObject.Parse(jsonResult);
					JArray items = (JArray)jsonObject["items"];

					// Create a list to hold YouTubeVideo objects
					var videoList = new List<YouTubeVideo>();

					// Loop through the items and add video information to the list
					foreach (var item in items)
					{
						string videoId = item["snippet"]["resourceId"]["videoId"].ToString();
						string videoTitle = item["snippet"]["title"].ToString();
						string thumbnailUrl = item["snippet"]["thumbnails"]["default"]["url"].ToString();

						// Create a new YouTubeVideo object and add it to the list
						var video = new YouTubeVideo
						{
							VideoId = videoId,
							Title = videoTitle,
							Thumbnail = thumbnailUrl
						};

						videoList.Add(video);
					}

					// Now the videoList contains all the video information from the playlist.
					// Here, you can perform additional processing or push the videoList to Firebase as needed.
					// For example, you can use the _firebaseClient to push the data to Firebase:
					_firebaseClient.Push("test-youtube", videoList);

					TempData["SuccessMessage"] = "Playlist has been successfully scraped!";
					// After processing the playlist, pass the videoList to the view.
					return Redirect("Course");
				}
			}
			catch (Exception ex)
			{
				// Handle any errors that may occur during the process.
				// You can log the error, show an error message, or handle it as needed.
				return View("Error"); // For example, show an error view.
			}
		}




		public ActionResult Playlists()
		{
			return View();
		}


	}
}