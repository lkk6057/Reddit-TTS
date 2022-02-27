using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    class Post
    {
        public string subreddit;
        public string title;
        public string author;
        public int score;
        public string body;
        public long unixTime;
        public List<Award> awards;
        public Comment[] comments;
        public int totalCommentCount;
        public Post(string subreddit,string title, string author, int score, string body, long unixTime, List<Award> awards, Comment[] comments, int totalCommentCount)
        {
            this.subreddit = subreddit;
            this.title = title;
            this.author = author;
            this.score = score;
            this.body = body;
            this.unixTime = unixTime;
            this.awards = awards;
            this.comments = comments;
            this.totalCommentCount = totalCommentCount;
        }
    }
}
