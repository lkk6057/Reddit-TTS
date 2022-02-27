using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    static class NumberUtility
    {
        public static string AbbreviatedNumber(int baseNumber)
        {
            string abbreviated = baseNumber.ToString();
            if (baseNumber>1000000)
            {
                abbreviated = Math.Round((double)baseNumber/1000000,1).ToString()+"m";
            }
            else if (baseNumber>1000)
            {
                abbreviated = Math.Round((double)baseNumber / 1000, 1).ToString() + "k";
            }
            return abbreviated;
        }
    }
}
