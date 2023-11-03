using System.Collections.Generic;

namespace AUST_BUDDY_WEB.Models
{
    public class PlaylistDetails
    {
        public string PlaylistId { get; set; }
        public string PlaylistTitle { get; set; }
        public string VideosKey { get; set; }
        public List<AUST_BUDDY_WEB.Models.PlaylistDetails> Playlists { get; set; }

    }
   
}