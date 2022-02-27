using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTTS.Modules
{
    static class Command
    {
        public static void Execute(string program, string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = program;
            process.StartInfo.Arguments = command;
            process.Start();
        }
        public static void ExecuteSequential(string program, List<string> commands)
        {
            foreach (string command in commands)
            {
                Process process = new Process();
                process.StartInfo.FileName = program;
                process.StartInfo.Arguments = command;
                process.Start();
                process.WaitForExit();
            }
        }
        public static void ExecuteParallel(string program, List<string> commands)
        {
            Parallel.ForEach(commands, command => {
                Process process = new Process();
                process.StartInfo.FileName = program;
                process.StartInfo.Arguments = command;
                process.Start();
                process.WaitForExit();
            });
        }
    }
}
