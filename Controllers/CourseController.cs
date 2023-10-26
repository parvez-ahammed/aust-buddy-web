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
        [Authorize]
        public ActionResult Index()
        {
            return View();
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

        [Authorize]
        public ActionResult Course()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult AddPlaylist()
        {
            return View();
        }
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize]
        public ActionResult Choose(int semester)
        {
            List<Course> courseDataList;
            string path = CourseUtility.GetYearSemester(semester);
            courseDataList = GetCourseDataFromFirebase(path);
            return View(courseDataList);
        }

        private readonly string playlistId = "PLom9DLdqyyk7r98iyUR-QAm6OeMijt1ud";
        [Authorize]
        public ActionResult Watch(string videos)
        {

            // Use the videos key to fetch the corresponding video data from Firebase
            FirebaseResponse response = _firebaseClient.Get($"youtube-videos/{videos}");
            var videoList = response.ResultAs<List<YouTubeVideo>>();

            // Pass the video data to the "Watch" view
            return View(videoList);
        }



        // ... (other methods)

        [HttpPost]
        public async Task<ActionResult> ScrapPlaylist(string playlistId, string playlistTitle)
        {
            try
            {
                var playlistDetails = new PlaylistDetails
                {
                    PlaylistId = playlistId,
                    PlaylistTitle = playlistTitle, // You can set the playlist title based on the fetched data
                                                   // Add other playlist details as needed
                };

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

                    
                    // Here, you can perform additional processing or push the videoList to Firebase as needed.
                    // For example, you can use the _firebaseClient to push the data to Firebase:

                    var videosKey = _firebaseClient.Push("youtube-videos", videoList).Result.name;


                    playlistDetails.VideosKey = videosKey;

                    _firebaseClient.Push("youtube-playlists", playlistDetails);






                    TempData["SuccessMessage"] = "Playlist has been successfully scraped!";
                    // After processing the playlist, pass the videoList to the view.
                    return Redirect("Course");
                }
            }
            catch (Exception)
            {
                
                return View("Error"); // For example, show an error view.
            }
        }



        [Authorize]
        public ActionResult Playlists()
        {
            List<PlaylistDetails> playLists;
            string path = "youtube-playlists";
            playLists = GetPlayListDataFromFirebase(path);
            return View(playLists);
        }

        private List<PlaylistDetails> GetPlayListDataFromFirebase(string path)
        {
            // Replace "courses" with the path to your Firebase collection where course data is stored.
            FirebaseResponse response = _firebaseClient.Get(path);

            // Check if the data is null or not found
            if (response.Body == "null")
            {
                return new List<PlaylistDetails>();
            }

            var playLists = response.ResultAs<Dictionary<string, PlaylistDetails>>();
            return playLists.Values.ToList();
        }


    }
}