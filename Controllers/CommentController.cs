using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AUST_BUDDY_WEB.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        private readonly IFirebaseConfig _firebaseConfig;
        private readonly IFirebaseClient _firebaseClient;

        public CommentController()
        {
            _firebaseConfig = new FirebaseConfig
            {
                AuthSecret = "fbs0BK9VcSR6Y2xZ6CSjA0JIK1S8yBCaVFQ00aXK",
                BasePath = "https://aust-buddies-default-rtdb.asia-southeast1.firebasedatabase.app"
            };
            _firebaseClient = new FireSharp.FirebaseClient(_firebaseConfig);
        }

        [HttpPost]
        public ActionResult Add(string announcementId, string commentText)
        {
            // Save the comment under the specified announcement ID in Firebase
            var commentData = new
            {
                AnnouncementId = announcementId,
                Text = commentText,
                // Other comment-related properties if needed
            };

            // Push the comment data to Firebase under a specific path
            _firebaseClient.Push($"comments/{announcementId}/", commentData);

            // After saving the comment, redirect to the announcement details page
            return RedirectToAction("details", "announcement", new { id = announcementId });
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}