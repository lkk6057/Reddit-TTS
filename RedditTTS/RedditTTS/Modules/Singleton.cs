using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    static class Singleton
    {
        public static MainForm mainForm;
        public static string postTitle = "Untitled";
        public static string imageFileFormat = "png";
        public static int backgroundUrl = 0;
        public static string[] backgroundUrls;
        public static List<int> AllIndexClumpsOf(string fullString, string searchString)
        {
            var foundIndexes = new List<int>();

            for (int i = fullString.IndexOf(searchString); i > -1; i = fullString.IndexOf(searchString, i + 1))
            {
                foundIndexes.Add(i);
            }
            List<int> trueIndexes = new List<int>();
            if (foundIndexes.Count > 0)
            {
                trueIndexes.Add(foundIndexes[0]);
                for (int i = 1; i < foundIndexes.Count; i++)
                {
                    if (foundIndexes[i - 1] != foundIndexes[i] - searchString.Length)
                    {
                        trueIndexes.Add(foundIndexes[i]);
                    }
                }
            }
            return trueIndexes;
        }
        public static string[] SplitAt(this string source, params int[] index)
        {
            index = index.Distinct().OrderBy(x => x).ToArray();
            string[] output = new string[index.Length + 1];
            int pos = 0;

            for (int i = 0; i < index.Length; pos = index[i++])
                output[i] = source.Substring(pos, index[i] - pos);

            output[index.Length] = source.Substring(pos);
            return output;
        }

    }
}
