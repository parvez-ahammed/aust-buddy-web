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


        private Announcement GetSingleAnnouncement(string path)
        {
            FirebaseResponse response = _firebaseClient.Get(path);

            // Check if the data is null or not found
            if (response.Body == "null")
            {
                return null; // Return null instead of creating a new Announcement instance
            }

            // Deserialize the response body to an Announcement object
            Announcement announcement = response.ResultAs<Announcement>();

            return announcement;
        }

        public List<Comment> GetCommentsByAnnouncementId(string announcementId)
        {



            // Replace this with your actual Firebase logic to fetch comments by announcement ID
            FirebaseResponse response = _firebaseClient.Get($"comments/{announcementId}/");

            if (response.Body == "null")
            {
                // Deserialize comments from the response body (replace with your deserialization logic)
                return new List<Comment>();

            }
            var comments = response.ResultAs<Dictionary<string, Comment>>();
            return comments.Values.ToList();
        }


        public ActionResult Details(string id)
        {
            List<Comment> comments = GetCommentsByAnnouncementId(id);

            string path = "public-announcements/all/" + id;

            Announcement announcement = GetSingleAnnouncement(path);

            var viewModel = new AnnouncementCommentViewModel
            {
                Announcement = announcement,
                Comments = comments
            };

            return View(viewModel);
        }
    }
}