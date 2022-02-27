namespace RedditTTS
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.idInputLabel = new System.Windows.Forms.Label();
            this.idInput = new System.Windows.Forms.TextBox();
            this.commentAmountLabel = new System.Windows.Forms.Label();
            this.commentAmount = new System.Windows.Forms.NumericUpDown();
            this.idSubmit = new System.Windows.Forms.Button();
            this.commentSelector = new System.Windows.Forms.CheckedListBox();
            this.outputNameLabel = new System.Windows.Forms.Label();
            this.outputFileName = new System.Windows.Forms.TextBox();
            this.generateVideo = new System.Windows.Forms.Button();
            this.toggleSelect = new System.Windows.Forms.Button();
            this.listControlsLabel = new System.Windows.Forms.Label();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.resetOrder = new System.Windows.Forms.Button();
            this.cycleSort = new System.Windows.Forms.Button();
            this.sortTypesSelector = new System.Windows.Forms.ListBox();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.exportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.importComments = new System.Windows.Forms.Button();
            this.exportComments = new System.Windows.Forms.Button();
            this.backgroundImageLabel = new System.Windows.Forms.Label();
            this.backgroundImageIndex = new System.Windows.Forms.NumericUpDown();
            this.backgroundPreview = new System.Windows.Forms.PictureBox();
            this.backgroundRandom = new System.Windows.Forms.Button();
            this.whiteTransparency = new System.Windows.Forms.CheckBox();
            this.backgroundMusicInput = new System.Windows.Forms.TextBox();
            this.backgroundMusicLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.commentAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundImageIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // idInputLabel
            // 
            this.idInputLabel.AutoSize = true;
            this.idInputLabel.Location = new System.Drawing.Point(41, 24);
            this.idInputLabel.Name = "idInputLabel";
            this.idInputLabel.Size = new System.Drawing.Size(79, 13);
            this.idInputLabel.TabIndex = 0;
            this.idInputLabel.Text = "Reddit Post ID:";
            // 
            // idInput
            // 
            this.idInput.Location = new System.Drawing.Point(126, 21);
            this.idInput.Name = "idInput";
            this.idInput.Size = new System.Drawing.Size(179, 20);
            this.idInput.TabIndex = 1;
            // 
            // commentAmountLabel
            // 
            this.commentAmountLabel.AutoSize = true;
            this.commentAmountLabel.Location = new System.Drawing.Point(419, 24);
            this.commentAmountLabel.Name = "commentAmountLabel";
            this.commentAmountLabel.Size = new System.Drawing.Size(85, 13);
            this.commentAmountLabel.TabIndex = 2;
            this.commentAmountLabel.Text = "Comment Count:";
            // 
            // commentAmount
            // 
            this.commentAmount.Location = new System.Drawing.Point(510, 22);
            this.commentAmount.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.commentAmount.Name = "commentAmount";
            this.commentAmount.Size = new System.Drawing.Size(47, 20);
            this.commentAmount.TabIndex = 3;
            this.commentAmount.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.commentAmount.ValueChanged += new System.EventHandler(this.commentAmount_ValueChanged);
            // 
            // idSubmit
            // 
            this.idSubmit.Location = new System.Drawing.Point(312, 18);
            this.idSubmit.Name = "idSubmit";
            this.idSubmit.Size = new System.Drawing.Size(75, 23);
            this.idSubmit.TabIndex = 4;
            this.idSubmit.Text = "Submit";
            this.idSubmit.UseVisualStyleBackColor = true;
            this.idSubmit.Click += new System.EventHandler(this.idSubmit_Click);
            // 
            // commentSelector
            // 
            this.commentSelector.FormattingEnabled = true;
            this.commentSelector.HorizontalScrollbar = true;
            this.commentSelector.Location = new System.Drawing.Point(32, 221);
            this.commentSelector.Name = "commentSelector";
            this.commentSelector.Size = new System.Drawing.Size(1487, 619);
            this.commentSelector.TabIndex = 5;
            this.commentSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commentSelector_KeyDown);
            this.commentSelector.KeyUp += new System.Windows.Forms.KeyEventHandler(this.commentSelector_KeyUp);
            // 
            // outputNameLabel
            // 
            this.outputNameLabel.AutoSize = true;
            this.outputNameLabel.Location = new System.Drawing.Point(29, 59);
            this.outputNameLabel.Name = "outputNameLabel";
            this.outputNameLabel.Size = new System.Drawing.Size(92, 13);
            this.outputNameLabel.TabIndex = 6;
            this.outputNameLabel.Text = "Output File Name:";
            // 
            // outputFileName
            // 
            this.outputFileName.Location = new System.Drawing.Point(127, 56);
            this.outputFileName.Name = "outputFileName";
            this.outputFileName.Size = new System.Drawing.Size(418, 20);
            this.outputFileName.TabIndex = 7;
            // 
            // generateVideo
            // 
            this.generateVideo.Location = new System.Drawing.Point(551, 54);
            this.generateVideo.Name = "generateVideo";
            this.generateVideo.Size = new System.Drawing.Size(98, 23);
            this.generateVideo.TabIndex = 8;
            this.generateVideo.Text = "Generate Video";
            this.generateVideo.UseVisualStyleBackColor = true;
            this.generateVideo.Click += new System.EventHandler(this.generateVideo_Click);
            // 
            // toggleSelect
            // 
            this.toggleSelect.Location = new System.Drawing.Point(32, 94);
            this.toggleSelect.Name = "toggleSelect";
            this.toggleSelect.Size = new System.Drawing.Size(179, 23);
            this.toggleSelect.TabIndex = 9;
            this.toggleSelect.Text = "Select/Deselect All (A)";
            this.toggleSelect.UseVisualStyleBackColor = true;
            this.toggleSelect.Click += new System.EventHandler(this.toggleSelect_Click);
            // 
            // listControlsLabel
            // 
            this.listControlsLabel.AutoSize = true;
            this.listControlsLabel.Location = new System.Drawing.Point(18, 123);
            this.listControlsLabel.Name = "listControlsLabel";
            this.listControlsLabel.Size = new System.Drawing.Size(311, 13);
            this.listControlsLabel.TabIndex = 10;
            this.listControlsLabel.Text = "Check/Uncheck (CTRL/ENTER)    Reorder (SHIFT+ARROWS)";
            // 
            // commentsLabel
            // 
            this.commentsLabel.AutoSize = true;
            this.commentsLabel.Location = new System.Drawing.Point(29, 205);
            this.commentsLabel.Name = "commentsLabel";
            this.commentsLabel.Size = new System.Drawing.Size(59, 13);
            this.commentsLabel.TabIndex = 11;
            this.commentsLabel.Text = "Comments:";
            // 
            // resetOrder
            // 
            this.resetOrder.Location = new System.Drawing.Point(217, 94);
            this.resetOrder.Name = "resetOrder";
            this.resetOrder.Size = new System.Drawing.Size(104, 23);
            this.resetOrder.TabIndex = 12;
            this.resetOrder.Text = "Reset Order (R)";
            this.resetOrder.UseVisualStyleBackColor = true;
            // 
            // cycleSort
            // 
            this.cycleSort.Location = new System.Drawing.Point(327, 94);
            this.cycleSort.Name = "cycleSort";
            this.cycleSort.Size = new System.Drawing.Size(131, 23);
            this.cycleSort.TabIndex = 13;
            this.cycleSort.Text = "Cycle Sorting Mode (D)";
            this.cycleSort.UseVisualStyleBackColor = true;
            this.cycleSort.Click += new System.EventHandler(this.cycleSort_Click);
            // 
            // sortTypesSelector
            // 
            this.sortTypesSelector.FormattingEnabled = true;
            this.sortTypesSelector.Location = new System.Drawing.Point(335, 123);
            this.sortTypesSelector.Name = "sortTypesSelector";
            this.sortTypesSelector.Size = new System.Drawing.Size(120, 82);
            this.sortTypesSelector.TabIndex = 15;
            this.sortTypesSelector.SelectedIndexChanged += new System.EventHandler(this.sortTypesSelector_SelectedIndexChanged);
            // 
            // importFileDialog
            // 
            this.importFileDialog.FileName = "openFileDialog1";
            this.importFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.importFileDialog_FileOk);
            // 
            // importComments
            // 
            this.importComments.Location = new System.Drawing.Point(230, 153);
            this.importComments.Name = "importComments";
            this.importComments.Size = new System.Drawing.Size(99, 23);
            this.importComments.TabIndex = 16;
            this.importComments.Text = "Import Comments";
            this.importComments.UseVisualStyleBackColor = true;
            this.importComments.Click += new System.EventHandler(this.importComments_Click);
            // 
            // exportComments
            // 
            this.exportComments.Location = new System.Drawing.Point(230, 182);
            this.exportComments.Name = "exportComments";
            this.exportComments.Size = new System.Drawing.Size(99, 23);
            this.exportComments.TabIndex = 17;
            this.exportComments.Text = "Export Comments";
            this.exportComments.UseVisualStyleBackColor = true;
            this.exportComments.Click += new System.EventHandler(this.exportComments_Click);
            // 
            // backgroundImageLabel
            // 
            this.backgroundImageLabel.AutoSize = true;
            this.backgroundImageLabel.Location = new System.Drawing.Point(479, 99);
            this.backgroundImageLabel.Name = "backgroundImageLabel";
            this.backgroundImageLabel.Size = new System.Drawing.Size(100, 13);
            this.backgroundImageLabel.TabIndex = 18;
            this.backgroundImageLabel.Text = "Background Image:";
            // 
            // backgroundImageIndex
            // 
            this.backgroundImageIndex.Location = new System.Drawing.Point(482, 123);
            this.backgroundImageIndex.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.backgroundImageIndex.Name = "backgroundImageIndex";
            this.backgroundImageIndex.Size = new System.Drawing.Size(97, 20);
            this.backgroundImageIndex.TabIndex = 19;
            this.backgroundImageIndex.ValueChanged += new System.EventHandler(this.backgroundImageIndex_ValueChanged);
            // 
            // backgroundPreview
            // 
            this.backgroundPreview.Location = new System.Drawing.Point(655, 8);
            this.backgroundPreview.Name = "backgroundPreview";
            this.backgroundPreview.Size = new System.Drawing.Size(348, 210);
            this.backgroundPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.backgroundPreview.TabIndex = 20;
            this.backgroundPreview.TabStop = false;
            // 
            // backgroundRandom
            // 
            this.backgroundRandom.Location = new System.Drawing.Point(461, 149);
            this.backgroundRandom.Name = "backgroundRandom";
            this.backgroundRandom.Size = new System.Drawing.Size(145, 23);
            this.backgroundRandom.TabIndex = 21;
            this.backgroundRandom.Text = "Random Background (X)";
            this.backgroundRandom.UseVisualStyleBackColor = true;
            this.backgroundRandom.Click += new System.EventHandler(this.backgroundRandom_Click);
            // 
            // whiteTransparency
            // 
            this.whiteTransparency.AutoSize = true;
            this.whiteTransparency.Location = new System.Drawing.Point(1009, 8);
            this.whiteTransparency.Name = "whiteTransparency";
            this.whiteTransparency.Size = new System.Drawing.Size(134, 17);
            this.whiteTransparency.TabIndex = 22;
            this.whiteTransparency.Text = "White to Transparency";
            this.whiteTransparency.UseVisualStyleBackColor = true;
            // 
            // backgroundMusicInput
            // 
            this.backgroundMusicInput.Location = new System.Drawing.Point(1009, 79);
            this.backgroundMusicInput.Multiline = true;
            this.backgroundMusicInput.Name = "backgroundMusicInput";
            this.backgroundMusicInput.Size = new System.Drawing.Size(479, 136);
            this.backgroundMusicInput.TabIndex = 23;
            // 
            // backgroundMusicLabel
            // 
            this.backgroundMusicLabel.AutoSize = true;
            this.backgroundMusicLabel.Location = new System.Drawing.Point(1006, 59);
            this.backgroundMusicLabel.Name = "backgroundMusicLabel";
            this.backgroundMusicLabel.Size = new System.Drawing.Size(309, 13);
            this.backgroundMusicLabel.TabIndex = 24;
            this.backgroundMusicLabel.Text = "Sequential Background Music URL/Paths (Newline for mutliple):";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1531, 852);
            this.Controls.Add(this.backgroundMusicLabel);
            this.Controls.Add(this.backgroundMusicInput);
            this.Controls.Add(this.whiteTransparency);
            this.Controls.Add(this.backgroundRandom);
            this.Controls.Add(this.backgroundPreview);
            this.Controls.Add(this.backgroundImageIndex);
            this.Controls.Add(this.backgroundImageLabel);
            this.Controls.Add(this.exportComments);
            this.Controls.Add(this.importComments);
            this.Controls.Add(this.sortTypesSelector);
            this.Controls.Add(this.cycleSort);
            this.Controls.Add(this.resetOrder);
            this.Controls.Add(this.commentsLabel);
            this.Controls.Add(this.listControlsLabel);
            this.Controls.Add(this.toggleSelect);
            this.Controls.Add(this.generateVideo);
            this.Controls.Add(this.outputFileName);
            this.Controls.Add(this.outputNameLabel);
            this.Controls.Add(this.commentSelector);
            this.Controls.Add(this.idSubmit);
            this.Controls.Add(this.commentAmount);
            this.Controls.Add(this.commentAmountLabel);
            this.Controls.Add(this.idInput);
            this.Controls.Add(this.idInputLabel);
            this.Name = "MainForm";
            this.Text = "Reddit Video Generator";
            ((System.ComponentModel.ISupportInitialize)(this.commentAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundImageIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label idInputLabel;
        private System.Windows.Forms.TextBox idInput;
        private System.Windows.Forms.Label commentAmountLabel;
        private System.Windows.Forms.NumericUpDown commentAmount;
        private System.Windows.Forms.Button idSubmit;
        private System.Windows.Forms.CheckedListBox commentSelector;
        private System.Windows.Forms.Label outputNameLabel;
        private System.Windows.Forms.TextBox outputFileName;
        private System.Windows.Forms.Button generateVideo;
        private System.Windows.Forms.Button toggleSelect;
        private System.Windows.Forms.Label listControlsLabel;
        private System.Windows.Forms.Label commentsLabel;
        private System.Windows.Forms.Button resetOrder;
        private System.Windows.Forms.Button cycleSort;
        private System.Windows.Forms.ListBox sortTypesSelector;
        private System.Windows.Forms.OpenFileDialog importFileDialog;
        private System.Windows.Forms.SaveFileDialog exportFileDialog;
        private System.Windows.Forms.Button importComments;
        private System.Windows.Forms.Button exportComments;
        private System.Windows.Forms.Label backgroundImageLabel;
        private System.Windows.Forms.NumericUpDown backgroundImageIndex;
        private System.Windows.Forms.PictureBox backgroundPreview;
        private System.Windows.Forms.Button backgroundRandom;
        private System.Windows.Forms.CheckBox whiteTransparency;
        private System.Windows.Forms.TextBox backgroundMusicInput;
        private System.Windows.Forms.Label backgroundMusicLabel;
    }
}

