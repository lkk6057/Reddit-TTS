using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    class PostVideo
    {
        public string imagePath;
        public string audioPath;
        public string videoPath;
        public PostVideo(string imagePath, string audioPath, string videoPath)
        {
            this.imagePath = imagePath;
            this.audioPath = audioPath;
            this.videoPath = videoPath;
        }
    }
}
