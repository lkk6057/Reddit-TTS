using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedditTTS.Modules
{
    static class VideoEdit
    {
        static string directory;
        static string ffmpeg;
        static string temp;
        public static void Initialize()
        {
            directory = Application.StartupPath;
            ffmpeg = Path.Combine(directory, Config.Get("ffmpegPath"));
            temp = Path.Combine(directory, Config.Get("tempPath") + "/");
        }
        public static void CreateVideo(PostState postState,CommentState[] commentStates)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Initializing Video Editing");
            List<string> commands = new List<string>();
            List<string> primaryCommands = new List<string>();
            List<CommentVideo> commentVideos = new List<CommentVideo>();

            string titleAudioLink = AudioEngine.GenerateAudioPath(postState.rawPost.title);
            PostVideo postVideo;
            if (titleAudioLink!=null)
            {
                string postVideoFileName = DataPersistence.MakeValidFileName($"{postState.rawPost.author}_post_titlecard.{Config.Get("videoFormat")}");
                string postVideoFilePath = Path.Combine(Config.Get("commentVideosPath"), postVideoFileName);
                string fullPostVideoFilePath = Path.Combine(directory, postVideoFilePath);
                postVideo = new PostVideo(postState.imagePath, titleAudioLink, fullPostVideoFilePath);
            }
            else
            {
                Console.WriteLine($"ERROR while generating TITLECARD audio: {postState.rawPost.title}");
                return;
            }
            for (int i = 0; i < commentStates.Length; i++)
            {
                CommentState commentState = commentStates[i];
                string commentPart = commentState.rawComment.parts[commentState.latestPart];
                string filteredCommentPart = commentPart.Replace("<br>","").Replace("<hidden>","").Replace("</hidden>","").Replace("<hyperlink>","").Replace("</hyperlink>","");
                string audioLink = AudioEngine.GenerateAudioPath(filteredCommentPart);
                if (audioLink != null)
                {
                    Console.WriteLine($"Generated {commentState.rawComment.author}'s TTS ({commentState.latestPart + 1}/{commentState.rawComment.parts.Count}) Line ({i + 1}/{commentStates.Length} Total): \"{commentPart}\"");

                    string videoFileName = DataPersistence.MakeValidFileName($"VIDEO_{commentState.rawComment.author}_{commentState.rawComment.score}_{commentState.latestPart + 1}-{commentState.rawComment.parts.Count}.{Config.Get("videoFormat")}");
                    string videoFilePath = Path.Combine(Config.Get("commentVideosPath"), videoFileName);
                    bool lastPart = commentState.rawComment.parts.Count == commentState.latestPart + 1;
                    string fullVideoFilePath = Path.Combine(directory, videoFilePath);
                    CommentVideo commentVideo = new CommentVideo(commentState.imagePath, audioLink, fullVideoFilePath,lastPart);
                    commentVideos.Add(commentVideo);
                }
                else
                {
                    Console.WriteLine($"ERROR Generating TTS Line {commentState.rawComment.author} {i + 1}/{commentStates.Length}: \"{commentState.latestPart}\"");
                    return;
                }
            }
            string intermissionFilePath = Config.Get("intermissionPath");
            string fullIntermissionFilePath = Path.Combine(directory,intermissionFilePath);
            List<string> videoPathsToCombine = new List<string>();
            primaryCommands.Add(ImageVideoCommand(postVideo.imagePath, postVideo.audioPath, postVideo.videoPath));
            videoPathsToCombine.Add(postVideo.videoPath);
            videoPathsToCombine.Add(fullIntermissionFilePath);
            foreach (CommentVideo commentVideo in commentVideos)
            {
                primaryCommands.Add(ImageVideoCommand(commentVideo.imagePath,commentVideo.audioPath,commentVideo.videoPath));
                videoPathsToCombine.Add(commentVideo.videoPath);
                if (commentVideo.lastPart)
                {
                    videoPathsToCombine.Add(fullIntermissionFilePath);
                }
            }

            string tempVideoOutputName = DataPersistence.MakeValidFileName($"Full {postState.rawPost.author}'s {postState.rawPost.subreddit} Post Video Without Music.{Config.Get("videoFormat")}");
            string tempVideoOutputPath = Path.Combine(Config.Get("tempPath"),tempVideoOutputName);
            string fullTempVideoOutputPath = Path.Combine(directory,tempVideoOutputPath);
            commands.Add(CombineVideoCommand(videoPathsToCombine, fullTempVideoOutputPath));

            string outputVideoName = Singleton.mainForm.GetVideoFileName();
            string outputVideoPath = Path.Combine(Config.Get("outputPath"),outputVideoName);
            string fullOutputVideoPath = Path.Combine(directory,outputVideoPath);

            string[] backgroundMusicPaths = Singleton.mainForm.GetBackgroundMusicPaths();
            if (backgroundMusicPaths.Length>0)
            {
                if (backgroundMusicPaths.Length>1)
                {
                    string combinedBackgroundAudiosName = DataPersistence.MakeValidFileName($"combined background audios.mp3");
                    string combinedBackgroundAudiosPath = Path.Combine(Config.Get("tempPath"),combinedBackgroundAudiosName);
                    string fullCombinedBackgroundAudiosPath = Path.Combine(directory,combinedBackgroundAudiosPath);
                    commands.Add(CombineAudiosCommand(backgroundMusicPaths.ToList(),fullCombinedBackgroundAudiosPath));
                    commands.Add(AddBackgroundMusicCommand(fullTempVideoOutputPath, fullCombinedBackgroundAudiosPath, fullOutputVideoPath));
                }
                else
                {
                    string backgroundAudioPath = backgroundMusicPaths[0];
                    commands.Add(AddBackgroundMusicCommand(fullTempVideoOutputPath, backgroundAudioPath, fullOutputVideoPath));
                }
            }
            else
            {
                commands.Add(CombineVideoCommand(videoPathsToCombine, fullOutputVideoPath));
            }
            File.WriteAllText("testCommands.txt", string.Join(Environment.NewLine, primaryCommands.Concat(commands)));
            System.Diagnostics.Process.Start("testCommands.txt");
            Command.ExecuteParallel(ffmpeg,primaryCommands);
            Command.ExecuteSequential(ffmpeg,commands);
            watch.Stop();
            Console.WriteLine($"Finished Generating Video With {commentStates.Length+1} parts in {watch.ElapsedMilliseconds} ms!");
        }
        static string ImageVideoCommand(string imagePath, string audioPath, string outputPath)
        {
            imagePath = $"\"{imagePath}\"";
            audioPath = $"\"{audioPath}\"";
            outputPath = $"\"{outputPath}\"";
            string command = $"-y -i {imagePath} -i {audioPath} -r 30 -ar 22050 -ac 1 -b:v 1M -b:a 192k -c:a copy {outputPath}";
            return command;
        }
        static string CombineVideoCommand(List<string> videoPaths, string outputPath)
        {
            outputPath = $"\"{outputPath}\"";
            string listPath = temp + "concat_list.txt";
            string listString;
            for (var i = 0; i < videoPaths.Count; i++)
            {
                videoPaths[i] = $"file '{videoPaths[i]}'";
            }
            listString = string.Join(Environment.NewLine, videoPaths);
            File.WriteAllText(listPath, listString);
            string command = $"-y -safe 0 -f concat -i \"{listPath}\" -c copy -ac 2 -ar 44100 -b:v 1M -b:a 192k {outputPath}";
            return command;
        }
        static string CombineAudiosCommand(List<string> audioPaths, string outputPath)
        {
            outputPath = $"\"{outputPath}\"";
            string audioPathString = string.Join("|", audioPaths);
            string command = $"-y -protocol_whitelist file,concat,http,https,tcp,tls,crypto -i \"concat: {audioPathString}\" -acodec copy {outputPath}";
            return command;
        }
        static string AddBackgroundMusicCommand(string videoPath, string audioPath, string outputPath, double volume = 1)
        {
            videoPath = $"\"{videoPath}\"";
            audioPath = $"\"{audioPath}\"";
            outputPath = $"\"{outputPath}\"";
            string command = $"-y -i {videoPath} -i {audioPath} -filter_complex \"[1:0]volume = {volume}[a1];[0:a][a1]amix = inputs = 2:duration = first\" -map 0:v:0 -r 30 -b:v 2M -b:a 192k -ac 2 -ar 44100 {outputPath}";
            return command;
        }
    }
}
