using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    class CommentVideo
    {
        public string imagePath;
        public string audioPath;
        public string videoPath;
        public bool lastPart;
        public CommentVideo(string imagePath, string audioPath, string videoPath,bool lastPart)
        {
            this.imagePath = imagePath;
            this.audioPath = audioPath;
            this.videoPath = videoPath;
            this.lastPart = lastPart;
        }
    }
}
