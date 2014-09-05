namespace RecursiveFileProcessor
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.numCharLimit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtCurrentFile = new System.Windows.Forms.TextBox();
            this.lblCurrentlyProcessing = new System.Windows.Forms.Label();
            this.btnViewResult = new System.Windows.Forms.Button();
            this.txtCheckedExtensions = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIgnoredExceptions = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFeedToFile = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lblSort = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCharLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(85, 13);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(291, 20);
            this.txtPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Project path:";
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(383, 13);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(112, 20);
            this.btnBrowseFile.TabIndex = 2;
            this.btnBrowseFile.Text = "Select file";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(420, 298);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 3;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // numCharLimit
            // 
            this.numCharLimit.Location = new System.Drawing.Point(85, 40);
            this.numCharLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCharLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCharLimit.Name = "numCharLimit";
            this.numCharLimit.Size = new System.Drawing.Size(84, 20);
            this.numCharLimit.TabIndex = 4;
            this.numCharLimit.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Char limit:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(383, 40);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(112, 20);
            this.btnBrowseFolder.TabIndex = 6;
            this.btnBrowseFolder.Text = "Select folder";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // txtCurrentFile
            // 
            this.txtCurrentFile.BackColor = System.Drawing.SystemColors.Control;
            this.txtCurrentFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentFile.Location = new System.Drawing.Point(117, 189);
            this.txtCurrentFile.Multiline = true;
            this.txtCurrentFile.Name = "txtCurrentFile";
            this.txtCurrentFile.ReadOnly = true;
            this.txtCurrentFile.Size = new System.Drawing.Size(372, 103);
            this.txtCurrentFile.TabIndex = 7;
            // 
            // lblCurrentlyProcessing
            // 
            this.lblCurrentlyProcessing.AutoSize = true;
            this.lblCurrentlyProcessing.Location = new System.Drawing.Point(12, 189);
            this.lblCurrentlyProcessing.Name = "lblCurrentlyProcessing";
            this.lblCurrentlyProcessing.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentlyProcessing.TabIndex = 8;
            // 
            // btnViewResult
            // 
            this.btnViewResult.Location = new System.Drawing.Point(339, 298);
            this.btnViewResult.Name = "btnViewResult";
            this.btnViewResult.Size = new System.Drawing.Size(75, 23);
            this.btnViewResult.TabIndex = 9;
            this.btnViewResult.Text = "View Result";
            this.btnViewResult.UseVisualStyleBackColor = true;
            this.btnViewResult.Click += new System.EventHandler(this.btnViewResult_Click);
            // 
            // txtCheckedExtensions
            // 
            this.txtCheckedExtensions.Location = new System.Drawing.Point(85, 67);
            this.txtCheckedExtensions.Name = "txtCheckedExtensions";
            this.txtCheckedExtensions.Size = new System.Drawing.Size(291, 20);
            this.txtCheckedExtensions.TabIndex = 10;
            this.txtCheckedExtensions.Text = ".cs,";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Extensions:";
            // 
            // txtIgnoredExceptions
            // 
            this.txtIgnoredExceptions.Location = new System.Drawing.Point(117, 96);
            this.txtIgnoredExceptions.Name = "txtIgnoredExceptions";
            this.txtIgnoredExceptions.Size = new System.Drawing.Size(259, 20);
            this.txtIgnoredExceptions.TabIndex = 12;
            this.txtIgnoredExceptions.Text = "designer.cs,";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Ignored extensions:";
            // 
            // btnFeedToFile
            // 
            this.btnFeedToFile.Location = new System.Drawing.Point(230, 298);
            this.btnFeedToFile.Name = "btnFeedToFile";
            this.btnFeedToFile.Size = new System.Drawing.Size(103, 23);
            this.btnFeedToFile.TabIndex = 14;
            this.btnFeedToFile.Text = "Feed result to file";
            this.btnFeedToFile.UseVisualStyleBackColor = true;
            this.btnFeedToFile.Click += new System.EventHandler(this.btnFeedToFile_Click);
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new System.Drawing.Point(54, 302);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(43, 13);
            this.lblSort.TabIndex = 15;
            this.lblSort.Text = "Sort by:";
            // 
            // cmbSort
            // 
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(103, 299);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(121, 21);
            this.cmbSort.TabIndex = 16;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 328);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.lblSort);
            this.Controls.Add(this.btnFeedToFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtIgnoredExceptions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCheckedExtensions);
            this.Controls.Add(this.btnViewResult);
            this.Controls.Add(this.lblCurrentlyProcessing);
            this.Controls.Add(this.txtCurrentFile);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numCharLimit);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPath);
            this.Name = "MainForm";
            this.Text = "File processor";
            ((System.ComponentModel.ISupportInitialize)(this.numCharLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.NumericUpDown numCharLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtCurrentFile;
        private System.Windows.Forms.Label lblCurrentlyProcessing;
        private System.Windows.Forms.Button btnViewResult;
        private System.Windows.Forms.TextBox txtCheckedExtensions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIgnoredExceptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFeedToFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.ComboBox cmbSort;
    }
}

