using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    class Substitution
    {
        public string identifier;
        public string replacement;
        public Substitution(string identifier, string replacement)
        {
            this.identifier = identifier;
            this.replacement = replacement;
        }
    }
}
