using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AUST_BUDDY_WEB.Models
{
    public class AnnouncementCommentViewModel
    {
        public Announcement Announcement { get; set; }
        public List<Comment> Comments { get; set; }
    }
}