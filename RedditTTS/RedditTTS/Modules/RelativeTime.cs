using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    class RelativeTime
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;
        public static string Ago(DateTime dateTime)
        {

            var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
            return CreateString(ts);
        }
        public static string Ago(long Utc)
        {
            var ts = TimeSpan.FromSeconds(DateTimeOffset.Now.ToUnixTimeSeconds() - Utc);
            return CreateString(ts);
        }
        static string CreateString(TimeSpan ts)
        {
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "1 second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "1 minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "1 hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "1 day ago";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "1 month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "1 year ago" : years + " years ago";
            }
        }
    }
}
