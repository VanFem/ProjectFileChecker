using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace RecursiveFileProcessor
{
    public partial class MainForm : Form
    {
        private readonly BackgroundWorker bw;
        private FileProcessor _fileProcessor;
        private ResultDisplay _resultDisplay;

        public MainForm()
        {
            InitializeComponent();
            bw = new BackgroundWorker {WorkerReportsProgress = true, WorkerSupportsCancellation = true};
            cmbSort.Items.Add("File");
            cmbSort.Items.Add("Length");
            cmbSort.Enabled = false;
            cmbSort.Visible = false;
            lblSort.Visible = false;
            btnViewResult.Enabled = false;
            btnViewResult.Visible = false;
            btnFeedToFile.Enabled = false;
            btnFeedToFile.Visible = false;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtCurrentFile.Text = _fileProcessor.CurrentFile;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done");
            btnProcess.Text = "Process";
            cmbSort.Enabled = true;
            cmbSort.Visible = true;
            lblSort.Visible = true;
            btnFeedToFile.Enabled = true;
            btnFeedToFile.Visible = true;
            btnViewResult.Enabled = true;
            btnViewResult.Visible = true;
            lblCurrentlyProcessing.Text = string.Empty;
            txtCurrentFile.Text = string.Format("Processed {0} out of {1} scanned files in {2} directories", _fileProcessor.ProcessedFiles, _fileProcessor.ScannedFiles, _fileProcessor.ScannedDirectories);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            _fileProcessor = new FileProcessor((int) numCharLimit.Value, bw, txtCheckedExtensions.Text,txtIgnoredExceptions.Text);
            if (File.Exists(txtPath.Text))
            {
                // This path is a file
                _fileProcessor.ProcessFile(txtPath.Text);
            }
            else if (Directory.Exists(txtPath.Text))
            {
                // This path is a directory
                _fileProcessor.ProcessDirectory(txtPath.Text);
            }
            else
            {
                MessageBox.Show(String.Format("{0} is not a valid file or directory.", txtPath.Text));
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                return;
            }
            if (File.Exists(txtPath.Text) || Directory.Exists(txtPath.Text))
            {
                bw.RunWorkerAsync();
                lblCurrentlyProcessing.Text = "Currently processing:";
                btnProcess.Text = "Cancel";
            }
            else
            {
                MessageBox.Show(String.Format("{0} is not a valid file or directory.", txtPath.Text));
            }
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnViewResult_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm is ResultDisplay)
                {
                    frm.Close();
                }
            }
            new ResultDisplay(_fileProcessor).Show();
        }

        private void btnFeedToFile_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult saveFileRes = saveFileDialog1.ShowDialog();
            if (saveFileRes == DialogResult.OK)
            {
                using (var f = new StreamWriter(saveFileDialog1.FileName))
                {
                    foreach (var res in _fileProcessor.ProcessingResult)
                    {
                        f.WriteLine("File: {0} Length:{2}\nLine: {1} :{3}\n", res.filePath, res.lineNumber, res.lineLength, res.line);
                    }
                }
            }
        }

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSort.SelectedIndex == 0)
            {
                _fileProcessor.ProcessingResult.Sort(
                    (p1, p2) => String.Compare(p1.filePath, p2.filePath, StringComparison.Ordinal));
            }
            else
            {
                _fileProcessor.ProcessingResult.Sort(
                    (p1, p2) => p2.lineLength.CompareTo(p1.lineLength));
            }

        }

    }
}
