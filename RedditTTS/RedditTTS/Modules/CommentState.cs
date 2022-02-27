using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace RedditTTS.Modules
{
    class CommentState
    {
        public Comment rawComment;
        public int latestPart;
        public string imagePath;
        public CommentState(Comment rawComment,int latestPart, string imagePath)
        {
            this.rawComment = rawComment;
            this.latestPart = latestPart;
            this.imagePath = imagePath;
        }
    }
}