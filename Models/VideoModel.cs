using System.Collections.Generic;

namespace AUST_BUDDY_WEB.Models
{
    public class VideoModel
    {
        public List<string> VideoTitles { get; } = new List<string>();
        public List<string> VideoEmbeds { get; } = new List<string>();
    }
}