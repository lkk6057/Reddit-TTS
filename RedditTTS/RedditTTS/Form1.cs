using RedditTTS.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedditTTS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }
        void Initialize()
        {
            Config.LoadConfig();
            VideoEdit.Initialize();
            Singleton.mainForm = this;
            LoadSortTypes();
            importFileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory,Config.Get("exportsPath"));
            importFileDialog.FileName = "comments";
            importFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            exportFileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, Config.Get("exportsPath"));
            exportFileDialog.FileName = "comments";
            exportFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            Singleton.imageFileFormat = Config.Get("imageFormat");
            Singleton.backgroundUrls = File.ReadAllLines(Config.Get("backgroundAssetsPath"));
            backgroundImageIndex.Maximum = Singleton.backgroundUrls.Length - 1;
            backgroundMusicInput.Text = Config.Get("defaultBackgroundMusic");
        }
        public int GetCommentAmount()
        {
            return (int)commentAmount.Value;
        }
        private void idSubmit_Click(object sender, EventArgs e)
        {
            VideoGenerator.InitializeVideo(idInput.Text);
        }
        public void SetOutputFileNameField(string postName)
        {
            string videoName = DataPersistence.MakeValidFileName(postName)+"."+Config.Get("videoFormat");
            string fileName = DataPersistence.MakeValidFileName(postName)+" comments";
            outputFileName.Text = videoName;
            exportFileDialog.FileName = fileName;
        }
        public string GetVideoFileName()
        {
            return outputFileName.Text;
        }
        bool shiftCommentPressed = false;
        Random random = new Random();
        private void commentSelector_KeyDown(object sender, KeyEventArgs e)
        {
            //Console.WriteLine(e.KeyValue);

            switch (e.KeyValue)
            {
                case 13:
                case 17:
                    {
                        if (commentSelector.SelectedIndex>-1)
                        {
                            bool targetCheck = !commentSelector.GetItemChecked(commentSelector.SelectedIndex);
                            commentSelector.SetItemChecked(commentSelector.SelectedIndex, targetCheck);
                        }
                        break;
                    }
                case 16:
                    {
                        shiftCommentPressed = true;
                        break;
                    }
                case 65:
                    {
                        ToggleSelect();
                        break;
                    }
                case 68:
                    {
                        CycleSort();
                        break;
                    }
                case 82:
                    {
                        ResetOrder();
                        break;
                    }
                case 88:
                    {
                        RandomBackground();
                        break;
                    }
                case 38:
                case 40:
                    {
                        if (shiftCommentPressed && commentSelector.SelectedItem != null)
                        {
                            int currentIndex = commentSelector.SelectedIndex;

                            int switchIndex = currentIndex - 1;
                            if (e.KeyValue==40)
                            {
                                switchIndex = currentIndex + 1;
                            }
                            if (switchIndex>=0&&switchIndex<VideoGenerator.commentSequence.Length)
                            {
                                VideoGenerator.SwapComments(currentIndex, switchIndex);
                                bool currentBool = commentSelector.GetItemChecked(currentIndex);
                                bool switchBool = commentSelector.GetItemChecked(switchIndex);
                                if (currentBool!=switchBool)
                                {
                                    commentSelector.SetItemChecked(currentIndex, !currentBool);
                                    commentSelector.SetItemChecked(switchIndex, !switchBool);
                                }
                                commentSelector.Items[currentIndex] = $"#{currentIndex} " + VideoGenerator.commentSequence[currentIndex].ToString();
                                commentSelector.Items[switchIndex] = $"#{switchIndex} " + VideoGenerator.commentSequence[switchIndex].ToString();
                                commentSelector.SelectedIndex = currentIndex;
                            }
                        }
                        break;
                    }
            }
        }
        
        private void commentSelector_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 16:
                    {
                        shiftCommentPressed = false;
                        break;
                    }
            }
        }
        private void toggleSelect_Click(object sender, EventArgs e)
        {
            ToggleSelect();
        }
        void ToggleSelect()
        {
            bool targetCheck = false;
            if (commentSelector.CheckedItems.Count == 0)
            {
                targetCheck = true;
            }
            for (var i = 0; i < commentSelector.Items.Count; i++)
            {
                commentSelector.SetItemChecked(i, targetCheck);
            }
        }
        public void PopulateComments()
        {
            for (var i = 0;i<VideoGenerator.commentSequence.Length;i++)
            {
                if (commentSelector.Items.Count<i+1)
                {
                    commentSelector.Items.Add("",true);
                }
                Comment comment = VideoGenerator.commentSequence[i];
                string commentString = $"#{i} "+comment.ToString();
                commentSelector.Items[i]=commentString;
                
            }
            while (commentSelector.Items.Count>VideoGenerator.commentSequence.Length)
            {
                commentSelector.Items.RemoveAt(commentSelector.Items.Count-1);
            }
            commentSelector.Focus();
        }
        void LoadSortTypes()
        {
            foreach (string sortType in VideoGenerator.sortTypes)
            {
                sortTypesSelector.Items.Add(sortType);
            }
            if (sortTypesSelector.Items.Count>0)
            {
                sortTypesSelector.SelectedIndex = 0;
            }
        }
        private void cycleSort_Click(object sender, EventArgs e)
        {
            CycleSort();
        }
        void CycleSort()
        {
            int nextType = VideoGenerator.currentSortType + 1;
            if (nextType > VideoGenerator.sortTypes.Length-1)
            {
                nextType = 0;
            }
            sortTypesSelector.SelectedIndex = nextType;
        }
        public void SetSortTypeSelector(int index)
        {
            sortTypesSelector.SelectedIndex = index;
        }
        private void sortTypesSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            VideoGenerator.SetSortType(sortTypesSelector.SelectedIndex);
        }
        void ResetOrder()
        {
            SetSortTypeSelector(0);
        }
        public bool GetCommentChecked(int index)
        {
            return commentSelector.GetItemChecked(index);
        }
        public void SetCommentChecked(int index,bool commentChecked)
        {
            commentSelector.SetItemChecked(index, commentChecked);
        }
        public void SetAllComments(bool commentChecked)
        {
            for (var i = 0; i < commentSelector.Items.Count; i++)
            {
                commentSelector.SetItemChecked(i, commentChecked);
            }
        }
        public void SetCommentStates(bool[] commentStates)
        {
            for (var i = 0;i<commentStates.Length;i++)
            {
                commentSelector.SetItemChecked(i,commentStates[i]);
            }
        }
        public bool[] GetCommentStates()
        {
            bool[] commentStates = new bool[commentSelector.Items.Count];
            for (var i = 0;i<commentStates.Length;i++) {
                commentStates[i] = commentSelector.GetItemChecked(i);
            }
            return commentStates;
        }

        private void importComments_Click(object sender, EventArgs e)
        {
            importFileDialog.ShowDialog();
        }
        private void importFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string jsonString;
            var fileStream = importFileDialog.OpenFile();

            using (StreamReader reader = new StreamReader(fileStream))
            {
                jsonString = reader.ReadToEnd();
            }
            DataPersistence.ImportCommentsJSON(jsonString);
            Console.WriteLine($"Successfully imported {VideoGenerator.commentSequence.Length} comments from '{importFileDialog.FileName}'!");
        }
        private void exportComments_Click(object sender, EventArgs e)
        {
            string jsonString = DataPersistence.GeneratePostJSON();
            if (exportFileDialog.ShowDialog() == DialogResult.OK)

            {

                StreamWriter writer = new StreamWriter(exportFileDialog.OpenFile());

                writer.Write(jsonString);

                writer.Dispose();

                writer.Close();

            }
            Console.WriteLine($"Successfully exported {VideoGenerator.commentSequence.Length} comments to '{exportFileDialog.FileName}'!");
        }



        private void generateVideo_Click(object sender, EventArgs e)
        {
            VideoEngine.GenerateVideo();
        }

        private void backgroundImageIndex_ValueChanged(object sender, EventArgs e)
        {
            Singleton.backgroundUrl = (int)backgroundImageIndex.Value;
            SetBackgroundPreviewLocation(Singleton.backgroundUrls[Singleton.backgroundUrl] + "?width=1920");
        }
        public void SetBackgroundPreviewLocation(string url)
        {
            backgroundPreview.ImageLocation = url;
        }
        public void SetBackgroundPreviewImage(Image image)
        {
            backgroundPreview.Image = image;
        }
        private void backgroundRandom_Click(object sender, EventArgs e)
        {
            RandomBackground();
        }
        void RandomBackground()
        {
            backgroundImageIndex.Value = random.Next(0, (int)backgroundImageIndex.Maximum);
        }
        public bool GetTransparency()
        {
            return whiteTransparency.Checked;
        }

        private void commentAmount_ValueChanged(object sender, EventArgs e)
        {
            string templateHTML = File.ReadAllText(Config.Get("postTemplatePath"));
            HtmlToImage.TestImage(templateHTML);
        }
        public string[] GetBackgroundMusicPaths()
        {
            return backgroundMusicInput.Text.Trim().Split(Environment.NewLine[0]);
        }
    }
}
