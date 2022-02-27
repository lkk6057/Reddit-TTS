using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    class Comment
    {
        public string author;
        public bool isOP;
        public int score;
        public string body;
        public long unixTime;
        public List<string> parts;
        public List<Award> awards;
        public Comment(string author, bool isOP, int score, string body, long unixTime, List<Award> awards = null)
        {
            this.author = author;
            this.isOP = isOP;
            this.score = score;
            this.body = body;
            this.unixTime = unixTime;
            this.awards = awards;
            parts = divideStringIntoParts(this.body);
            if (awards == null)
            {
                this.awards = new List<Award>();
            }
        }
        List<string> divideStringIntoParts(string bodyString)
        {
            List<string> newParts = new List<string>();
            List<string> firstParts = Regex.Split(bodyString, @"(?<!\w\.\w.)(?<![A-Z][a-z]\.)(?<=\.|\?|\!)\s").ToList<string>();
            if (firstParts.Count==0)
            {
                return newParts;
            }
            List<string> secondParts = new List<string>();
            foreach (string part in firstParts)
            {
                List<int> lineBreakClumps = Singleton.AllIndexClumpsOf(part, "<br>");
                if (lineBreakClumps.Count == 0)
                {
                    secondParts.Add(part);
                }
                else
                {
                    string[] lineBrokenParts = Singleton.SplitAt(part, lineBreakClumps.ToArray());
                    foreach (string lineBrokenPart in lineBrokenParts)
                    {
                        secondParts.Add(lineBrokenPart);
                    }
                }
            }
            if (secondParts.Count>0) {
                string currentPart = secondParts[0];
                bool shouldCombine = false;
                for (int i = 0; i < secondParts.Count; i++)
                {
                    if (shouldCombine)
                    {
                        currentPart += secondParts[i];
                    }
                    else
                    {
                        currentPart = secondParts[i];
                    }
                    string[] words = currentPart.ToLower().Split(" "[0]);
                    string lastWord = words.Last();
                    if (lastWord == "dr." || lastWord == "mr." || lastWord == "ms." || lastWord == "mrs." || lastWord == "jr." || lastWord == "sr." || lastWord == "prof." || lastWord == "dr." || lastWord == "eg." || lastWord == "e." || lastWord == "g." || lastWord == "vol." || lastWord == "etc.")
                    {
                        shouldCombine = true;
                    }
                    else
                    {
                        shouldCombine = false;
                    }
                    if (!shouldCombine)
                    {
                        newParts.Add(currentPart);
                    }
                }
                return newParts;
            }
            else
            {
                return firstParts;
            }
        }
        public override string ToString()
        {
            string warning = body.Length > 3500 ? "<WARNING> ABOVE 3500 CHAR LIMIT " : "";
            string commentString = $"{warning}↑{score}↓ (LENGTH:{body.Length}) (UNIX:{unixTime}) <{author}>: {body}";
            return commentString;
        }
    }
}
