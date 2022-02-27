using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace RedditTTS.Modules
{
    static class VideoEngine
    {
        public static void GenerateVideo()
        {
            PostState postState = GenerateTitleImage();
            Console.WriteLine("Generated Title Image.");

            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<Comment> videoComments = new List<Comment>();
            for (var i = 0; i < VideoGenerator.commentSequence.Length; i++)
            {
                if (Singleton.mainForm.GetCommentChecked(i))
                {
                    videoComments.Add(VideoGenerator.commentSequence[i]);
                }
            }


            List<QueuedCommentHTML> commentImageQueue = new List<QueuedCommentHTML>();
            for (var commentIndex = 0; commentIndex < videoComments.Count; commentIndex++)
            {
                Comment rawComment = videoComments[commentIndex];
                for (int partIndex = 0; partIndex < rawComment.parts.Count; partIndex++)
                {
                    string fileName = DataPersistence.MakeValidFileName($"{rawComment.author} {rawComment.score} {partIndex + 1}-{rawComment.parts.Count}");
                    string commentHTML = CommentToHTML(rawComment, partIndex);
                    QueuedCommentHTML queuedComment = new QueuedCommentHTML(fileName, commentHTML, rawComment, partIndex);
                    commentImageQueue.Add(queuedComment);
                }
            }

            CommentState[] commentStateArray = new CommentState[commentImageQueue.Count];

            int progressParts = 0;
            Parallel.For(0, commentImageQueue.Count, queuedCommentIndex =>
            {
                QueuedCommentHTML queuedComment = commentImageQueue[queuedCommentIndex];
                Comment rawComment = queuedComment.rawComment;
                byte[] imageBytes = HtmlToImage.Convert(queuedComment.commentHTML, int.Parse(Config.Get("imageWidth")), int.Parse(Config.Get("imageHeight")));
                var memoryStream = new MemoryStream(imageBytes);
                Image image = Image.FromStream(memoryStream);
                string filePath = Path.Combine(Config.Get("commentImagesPath"), queuedComment.fileName + "." + Singleton.imageFileFormat);
                image.Save(filePath);
                CommentState commentState = new CommentState(rawComment,queuedComment.currentPart,Path.Combine(Application.StartupPath,filePath));
                commentStateArray[queuedCommentIndex] = commentState;
                string percentProgress = (Math.Round(((progressParts + 1f) / commentImageQueue.Count) * 10000) / 100).ToString();
                Console.WriteLine($"Generated {rawComment.author}'s comment ({videoComments.IndexOf(rawComment) + 1}/{videoComments.Count}) {Singleton.imageFileFormat} part ({queuedComment.currentPart}/{rawComment.parts.Count}) [{progressParts + 1}/{commentImageQueue.Count}] [{percentProgress}%]");
                progressParts++;
            });
            watch.Stop();
            Console.WriteLine($"Finished Generating {videoComments.Count} Comments and {commentImageQueue.Count} Images in {watch.ElapsedMilliseconds} ms.");
            VideoEdit.CreateVideo(postState,commentStateArray);
        }
        static PostState GenerateTitleImage()
        {
            Post post = VideoGenerator.post;
            string postHTML = PostToHTML(post);
            byte[] imageBytes = HtmlToImage.Convert(postHTML, int.Parse(Config.Get("imageWidth")), int.Parse(Config.Get("imageHeight")));
            var memoryStream = new MemoryStream(imageBytes);
            Image image = Image.FromStream(memoryStream);
            string fileName = DataPersistence.MakeValidFileName($"_TitleCard");
            string filePath = Path.Combine(Config.Get("commentImagesPath"), fileName + "." + Singleton.imageFileFormat);
            image.Save(filePath);
            return new PostState(post,Path.Combine(Application.StartupPath,filePath));
        }
        public static string PostToHTML(Post post)
        {
            string postHTMLTemplate = File.ReadAllText(Config.Get("postTemplatePath"));
            string awardsHTML = AwardListToHtml(post.awards);
            List<Substitution> postSubstitutions = new List<Substitution>();
            postSubstitutions.Add(new Substitution("%SUBREDDIT%", post.subreddit));
            postSubstitutions.Add(new Substitution("%TITLE%", post.title));
            postSubstitutions.Add(new Substitution("%AUTHOR%", post.author));
            postSubstitutions.Add(new Substitution("%POINTS%", post.score.ToString()));
            postSubstitutions.Add(new Substitution("%AGO%", RelativeTime.Ago(post.unixTime)));
            postSubstitutions.Add(new Substitution("%BODY%", post.body));
            postSubstitutions.Add(new Substitution("%BACKGROUND%", Singleton.backgroundUrls[Singleton.backgroundUrl] + "?width=1920"));
            postSubstitutions.Add(new Substitution("%AWARDS%", awardsHTML));
            postSubstitutions.Add(new Substitution("%COMMENTS%", post.totalCommentCount.ToString()));
            string postHTMLBody = SubstituteString(postHTMLTemplate, postSubstitutions);
            return postHTMLBody;
        }
        public static string CommentToHTML(Comment comment, int part = 0)
        {
            string progressBody = "";
            if (part >= comment.parts.Count)
            {
                part = comment.parts.Count - 1;
            }
            for (int i = 0; i <= part; i++)
            {
                progressBody += comment.parts[i] + " ";
            }
            
            if (part+1<=comment.parts.Count)
            {
                progressBody += "<hidden>";
                for (int i = part+1; i < comment.parts.Count; i++)
                {
                    progressBody += comment.parts[i].Replace("<hyperlink>","<hidden>").Replace("</hyperlink>","</hidden>");
                }
                progressBody += "</hidden>";
            }
            string commentHTMLTemplate = File.ReadAllText(Config.Get("commentTemplatePath"));
            string awardsHTML = AwardListToHtml(comment.awards);
            List<Substitution> commentSubstitutions = new List<Substitution>();
            commentSubstitutions.Add(new Substitution("%AUTHOR%", comment.author));
            commentSubstitutions.Add(new Substitution("%POINTS%", NumberUtility.AbbreviatedNumber(comment.score)));
            commentSubstitutions.Add(new Substitution("%AGO%", RelativeTime.Ago(comment.unixTime)));
            commentSubstitutions.Add(new Substitution("%BODY%", progressBody));
            commentSubstitutions.Add(new Substitution("%BACKGROUND%", Singleton.backgroundUrls[Singleton.backgroundUrl] + "?width=1920"));
            commentSubstitutions.Add(new Substitution("%AWARDS%", awardsHTML));
            string commentHTMLBody = SubstituteString(commentHTMLTemplate,commentSubstitutions);
            return commentHTMLBody;
        }
        static string AwardListToHtml(List<Award> awards)
        {
            string awardsHTML = "";
            if (awards!=null) {
                foreach (Award award in awards)
                {
                    string amountString = award.amount > 1 ? $"{award.amount}&nbsp" : "";
                    string awardHTML = $"<img class = \"award\" src = \"{award.url}\"></img><span class = \"awardlabel\">{amountString}</span>";
                    awardsHTML += awardHTML;
                }
            }
            return awardsHTML;
        }
        public static string SubstituteString(string rawString,List<Substitution> substitutions)
        {
            string newString = rawString;
            foreach (Substitution substitution in substitutions)
            {
                newString = newString.Replace(substitution.identifier,substitution.replacement);
            }
            return newString.Trim();
        }
    }
}
