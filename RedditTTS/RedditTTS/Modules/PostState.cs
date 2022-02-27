using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    class PostState
    {
        public Post rawPost;
        public string imagePath;
        public PostState(Post rawPost, string imagePath)
        {
            this.rawPost = rawPost;
            this.imagePath = imagePath;
        }
    }
}
