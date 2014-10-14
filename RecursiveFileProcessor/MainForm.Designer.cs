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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblCurrentlyProcessing = new System.Windows.Forms.Label();
            this.txtCheckedExtensions = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIgnoredExceptions = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtCurrentFile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
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
            // lblCurrentlyProcessing
            // 
            this.lblCurrentlyProcessing.AutoSize = true;
            this.lblCurrentlyProcessing.Location = new System.Drawing.Point(12, 189);
            this.lblCurrentlyProcessing.Name = "lblCurrentlyProcessing";
            this.lblCurrentlyProcessing.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentlyProcessing.TabIndex = 8;
            // 
            // txtCheckedExtensions
            // 
            this.txtCheckedExtensions.Location = new System.Drawing.Point(85, 67);
            this.txtCheckedExtensions.Name = "txtCheckedExtensions";
            this.txtCheckedExtensions.Size = new System.Drawing.Size(291, 20);
            this.txtCheckedExtensions.TabIndex = 10;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(382, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 45);
            this.button1.TabIndex = 14;
            this.button1.Text = "Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(387, 282);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 34);
            this.button2.TabIndex = 15;
            this.button2.Text = "Process";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 328);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtIgnoredExceptions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCheckedExtensions);
            this.Controls.Add(this.lblCurrentlyProcessing);
            this.Controls.Add(this.txtCurrentFile);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPath);
            this.Name = "MainForm";
            this.Text = "File processor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lblCurrentlyProcessing;
        private System.Windows.Forms.TextBox txtCheckedExtensions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIgnoredExceptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox txtCurrentFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

