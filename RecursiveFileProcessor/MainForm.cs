using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using RecursiveFileProcessor.Kendo.Migration;

namespace RecursiveFileProcessor
{
    public partial class MainForm : Form
    {
        private readonly BackgroundWorker bw;
        private string result;
        private string _log;
        private string _currentFile;
        private MigrationProcessor _fileProcessor;
        private ResultDisplay _resultDisplay;
        private MigrationSettings _migrationSettings;

        public MainForm()
        {
            InitializeComponent();
            _migrationSettings = new MigrationSettings();
            bw = new BackgroundWorker {WorkerReportsProgress = true, WorkerSupportsCancellation = true};
            button2.Text = "Process";
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtCurrentFile.Text = string.Format("Processing {0}", _currentFile) ;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done");
            lblCurrentlyProcessing.Text = string.Empty;
            File.WriteAllText("~migrationLog.txt", _log);
            System.Diagnostics.Process.Start("~migrationLog.txt");
            txtCurrentFile.Text = string.Empty;
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            _fileProcessor = new MigrationProcessor
            {
                AppliedMigration = _migrationSettings.DialogResult == DialogResult.OK
                    ? _migrationSettings.GetMigration()
                    : _migrationSettings.GetFullMigration()
            };

            var logStringBuilder = new StringBuilder();

            foreach (var fileName in openFileDialog1.FileNames)
            {
                logStringBuilder.Append(_fileProcessor.ProcessFile(fileName));
            }

            _log = logStringBuilder.ToString();

            //if (File.Exists(txtPath.Text))
            //{
            //    // This path is a file
            //    _fileProcessor.ProcessFile(txtPath.Text);
            //}
            //else if (Directory.Exists(txtPath.Text))
            //{
            //    // This path is a directory
            //    _fileProcessor.ProcessDirectory(txtPath.Text);
            //}
            //else
            //{
            //    MessageBox.Show(String.Format("{0} is not a valid file or directory.", txtPath.Text));
            //}
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
                button2.Text = "Cancel";
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

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _migrationSettings.ShowDialog();
        }

    }
}
