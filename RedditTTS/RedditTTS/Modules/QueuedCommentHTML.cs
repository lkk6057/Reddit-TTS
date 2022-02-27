using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    class QueuedCommentHTML
    {
        public string fileName;
        public Comment rawComment;
        public string commentHTML;
        public int currentPart;
        public QueuedCommentHTML(string fileName, string commentHTML,Comment rawComment,int currentPart)
        {
            this.fileName = fileName;
            this.commentHTML = commentHTML;
            this.rawComment = rawComment;
            this.currentPart = currentPart;
        }
    }
}
