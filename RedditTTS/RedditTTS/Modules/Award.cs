using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    class Award
    {
        public string name;
        public string url;
        public int amount;
        public Award(string name, string url, int amount)
        {
            this.name = name;
            this.url = url;
            this.amount = amount;
        }
    }
}
