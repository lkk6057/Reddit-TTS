using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RedditTTS.Modules
{
    static class VideoGenerator
    {
        public static Post post;
        public static Comment[] commentSequence = new Comment[0];
        public static Comment[] backupSequence = new Comment[0];
        public static string[] sortTypes = { "None", "Karma", "Karma Ascending", "Oldest", "Newest" };
        public static int currentSortType = 0;
        public static void InitializeVideo(string postId)
        {
            Post newPost = RedditClient.GetPost(postId);
            SetPost(newPost);
            Singleton.mainForm.SetAllComments(true);
        }
        public static void SetPost(Post newPost)
        {
            post = newPost;
            SetComments(post.comments);
        }
        public static void SetComments(Comment[] comments)
        {
            commentSequence = comments;
            backupSequence = commentSequence;
            Singleton.mainForm.PopulateComments();
        }
        public static void SwapComments(int index1, int index2)
        {
            Comment tempComment = commentSequence[index1];
            commentSequence[index1] = commentSequence[index2];
            commentSequence[index2] = tempComment;
        }
        public static void SetSortType(int sortType)
        {
            currentSortType = sortType;
            Comment[] oldComments = new Comment[commentSequence.Length];
            commentSequence.CopyTo(oldComments,0);
            bool[] oldBools = Singleton.mainForm.GetCommentStates();
            switch (currentSortType)
            {
                case 0:
                    {
                        commentSequence = backupSequence;
                        break;
                    }
                case 1:
                    {
                        commentSequence = commentSequence.ToList().OrderByDescending(x => x.score).ToArray();
                        break;
                    }
                case 2:
                    {
                        commentSequence = commentSequence.ToList().OrderBy(x => x.score).ToArray();
                        break;
                    }
                case 3:
                    {
                        commentSequence = commentSequence.ToList().OrderBy(x => x.unixTime).ToArray();
                        break;
                    }
                case 4:
                    {
                        commentSequence = commentSequence.ToList().OrderByDescending(x => x.unixTime).ToArray();
                        break;
                    }

            }
            for (var i = 0;i<commentSequence.Length;i++)
            {
                int index = commentSequence.ToList().IndexOf(oldComments[i]);
                Singleton.mainForm.SetCommentChecked(index,oldBools[i]);
            }
            Singleton.mainForm.PopulateComments();
        }
    }
}
