using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RedditTTS.Modules
{
    static class DataPersistence
    {
        public static void ImportCommentsJSON(string json)
        {
            try
            {
                JObject jsonObject = JObject.Parse(json);
                Post post = jsonObject["post"].ToObject<Post>();

                Singleton.postTitle = post.title;
                Singleton.mainForm.SetOutputFileNameField(Singleton.postTitle);
                bool[] commentStates = ((JArray)jsonObject.GetValue("commentStates")).ToObject<bool[]>();
                VideoGenerator.SetPost(post);
                Singleton.mainForm.SetCommentStates(commentStates);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while Importing comments: "+ex.ToString());
            }
        }
        public static string GeneratePostJSON()
        {
            try
            {
                JObject jsonObject = new JObject();
                VideoGenerator.post.comments = VideoGenerator.commentSequence;
                jsonObject.Add("post",JToken.FromObject(VideoGenerator.post));
                JArray checkedArray = JArray.FromObject(Singleton.mainForm.GetCommentStates());
                jsonObject.Add("commentStates", checkedArray);
                return jsonObject.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error while Exporting comments: " + ex.ToString());
                return null;
            }
        }
        public static string MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }
    }
}
