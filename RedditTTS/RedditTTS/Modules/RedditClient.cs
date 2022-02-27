using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace RedditTTS.Modules
{
    static class RedditClient
    {
        static WebClient client = new WebClient();
        public static Post GetPost(string postId)
        {
            string response = client.DownloadString($"https://www.reddit.com/comments/{postId}.json");
            JArray submissionObj = JArray.Parse(response);
            JToken postObj = submissionObj[0]["data"]["children"][0]["data"];
            JArray commentArray = (JArray)submissionObj[1]["data"]["children"];
            JArray moreCommentsArray = (JArray)commentArray[commentArray.Count - 1]["data"]["children"];

            string subreddit = (string)postObj["subreddit_name_prefixed"];
            string title = (string)postObj["title"];
            Singleton.postTitle = title;
            string author = (string)postObj["author"];
            int score = (int)postObj["score"];
            string body = (string)postObj["selftext"];
            long unixTime = (long)postObj["created_utc"];
            List<Award> awards = JsonAwardsToAwardList((JArray)postObj["all_awardings"]);
            Comment[] comments = GetComments(submissionObj, postId);
            int moreCommentsCount = 0;
            if (moreCommentsArray!=null)
            {
                moreCommentsCount += moreCommentsArray.Count;
            }
            int totalCommentCount = comments.Length+moreCommentsCount;
            Post newPost = new Post(subreddit,title,author,score,body,unixTime,awards,comments,totalCommentCount);
            return newPost;
        }
        public static Comment[] GetComments(JArray submissionObj,string postId)
        {
            List<Comment> commentSequence = new List<Comment>();
            JArray commentArray = (JArray)submissionObj[1]["data"]["children"];
            Singleton.mainForm.SetOutputFileNameField(Singleton.postTitle);
            int iterateAmount = Singleton.mainForm.GetCommentAmount();

            if (iterateAmount > commentArray.Count - 2)
            {
                iterateAmount = commentArray.Count - 2;
            }
            Console.WriteLine("Max: "+commentArray.Count);
            Console.WriteLine("Limit: " + iterateAmount);
            commentSequence.Clear();
            for (int i = 0; i < iterateAmount; i++)
            {
                Comment newComment = JTokenToComment(commentArray[i]);
                commentSequence.Add(newComment);
            }
            int remaining = Singleton.mainForm.GetCommentAmount()-commentSequence.Count;
            Console.WriteLine("Remaining: " + remaining);
            JArray moreArray = (JArray)commentArray[commentArray.Count - 1]["data"]["children"];
            if (remaining > 0 && moreArray != null)
            {
                int moreIterate = remaining;
                if (moreIterate > moreArray.Count)
                {
                    moreIterate = moreArray.Count;
                }
                Console.WriteLine($"{remaining} comments short of target.");
                for (var i = 0; i < moreIterate; i++)
                {
                    Console.WriteLine($"Fetching Extra Comment {i + 1}/{moreIterate}");
                    Comment comment = GetMoreComment(postId, (string)moreArray[i]);
                    if (comment != null)
                    {
                        commentSequence.Add(comment);
                    }
                }
                Console.WriteLine("Finished fetching Extra Comments.");
            }
            return commentSequence.ToArray();
        }
        static Comment GetMoreComment(string postId,string commentId)
        {
            try
            {
                string response = client.DownloadString($"https://www.reddit.com/comments/{postId}/morecomments/{commentId}.json");
                JArray submissionObj = JArray.Parse(response);
                JToken comment = submissionObj[1]["data"]["children"][0];
                Comment newComment = JTokenToComment(comment);
                return newComment;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to Retrieve comment: " + ex.ToString());
                return null;
            }
        }
        static Comment JTokenToComment(JToken comment)
        {
            JToken commentData = comment["data"];
            string author = commentData["author"].ToString();
            bool isOP = bool.Parse(commentData["is_submitter"].ToString());
            int score = (int)commentData["score"];
            string body = HttpUtility.HtmlDecode(commentData["body"].ToString()).Replace("\n", " <br>").Replace(" <br> ", "<br>").Replace(" <br>", "<br>").Replace("<br> ", "<br>");
            Regex hyperLinkRegex = new Regex(@"(?:__|[*#])|\[(.*?)\]\(.*?\)");
            body = hyperLinkRegex.Replace(body, "<hyperlink>$1</hyperlink>");
            long unixTime = (long)commentData["created_utc"];
            List<Award> awards = new List<Award>();
            JArray jAwards = (JArray)comment["data"]["all_awardings"];
            if (jAwards!=null)
            {
                awards = JsonAwardsToAwardList(jAwards);
            }
            Comment newComment = new Comment(author, isOP, score, body, unixTime,awards);
            return newComment;
        }
        static List<Award> JsonAwardsToAwardList(JArray jAwards)
        {
            List<Award> awards = new List<Award>();
            foreach (JToken jAward in jAwards)
            {
                string name = (string)jAward["name"];
                string url = (string)jAward["icon_url"];
                int amount = (int)jAward["count"];
                Award award = new Award(name, url, amount);
                awards.Add(award);
            }
            return awards;
        }
    }
}
