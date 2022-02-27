using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedditTTS.Modules
{
    static class AudioEngine
    {
        public static string GenerateAudioPath(string audioString, int speed = 0, int pitch = 0, int pauses = 0)
        {
            SpeechSynthesizer reader = new SpeechSynthesizer();
            reader.Rate = (int)-2;
            Random random = new Random();
            string fileName = DataPersistence.MakeValidFileName(audioString.Split(" "[0])[0] + random.Next(0, 9999)+".mp3");
            string filePath = Path.Combine(Application.StartupPath, Path.Combine(Config.Get("commentAudiosPath"), fileName));
            reader.SetOutputToWaveFile(filePath);
            reader.Speak(audioString);
            return filePath;
        }
    }
}
