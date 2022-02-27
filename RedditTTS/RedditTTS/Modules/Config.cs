using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    static class Config
    {
        private static NameValueCollection config;
        private static string configFilePath = "Config/config.txt";
        public static void LoadConfig()
        {
            try
            {
                config = new NameValueCollection();
                config.Clear();
                string[] configFile = File.ReadAllLines(configFilePath);
                foreach (string line in configFile)
                {
                    if (line.Contains(":")&&line[0]!="/"[0])
                    {
                        string[] split = line.Split(":"[0]);
                        List<string> rest = new List<string>();
                        for (var i = 1;i<split.Length;i++)
                        {
                            rest.Add(split[i]);
                        }
                        string value = string.Join(":", rest);
                        config.Add(split[0], value);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while loading config: " + ex.ToString());
            }
        }
        public static string Get(string key)
        {
            return config.Get(key);
        }
        public static NameValueCollection GetCollection()
        {
            return config;
        }
    }
}
