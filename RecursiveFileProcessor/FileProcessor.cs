using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursiveFileProcessor
{
    public class FileProcessor
    {
        public string CurrentFile { get; private set; }

        public int CharLimit { get; set; }
        public int ScannedFiles { get; private set; }
        public int ProcessedFiles { get; private set; }
        public int ScannedDirectories { get; private set; }

        public List<string> CheckedExtensions { get; private set; }
        public List<string> IgnoredExtensions { get; private set; }


        public List<ResultEntry> ProcessingResult { get; private set; }

        private BackgroundWorker myWorker;

        public FileProcessor(int charLimit, BackgroundWorker worker, string checkedExtensions, string ignoredExtensions)
        {
            CharLimit = charLimit;
            CheckedExtensions = new List<string>();
            IgnoredExtensions = new List<string>();
            ScannedDirectories = 0;
            ScannedFiles = 0;
            ProcessedFiles = 0;
            var extensionList = checkedExtensions.Split(',');
            foreach (string t in extensionList)
            {
                if (!string.IsNullOrEmpty(t.Trim()))
                {
                    CheckedExtensions.Add(t.Trim());
                }
            }

            extensionList = ignoredExtensions.Split(',');
            foreach (string t in extensionList)
            {
                if (!string.IsNullOrEmpty(t.Trim()))
                {
                    IgnoredExtensions.Add(t.Trim());
                }
            }
            myWorker = worker;
            ProcessingResult = new List<ResultEntry>();
        }

        public void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory. 
            ScannedDirectories++;
            string[] fileEntries = {};
            try
            {
                 fileEntries = Directory.GetFiles(targetDirectory);
            }
            catch (Exception ex)
            {
                ProcessingResult.Add(new ResultEntry() { filePath = targetDirectory, line = ex.Message });
            }

            foreach (string fileName in fileEntries)
            {
                if (myWorker.CancellationPending) return;
                ProcessFile(fileName);
            }

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = {};
            try
            {
                subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            }
            catch (Exception ex)
            {
                ProcessingResult.Add(new ResultEntry() { filePath = targetDirectory, line = ex.Message });
            }

            foreach (string subdirectory in subdirectoryEntries)
            {
                if (myWorker.CancellationPending) return;
                ProcessDirectory(subdirectory);
            }
        }


        public void ProcessFile(string path)
        {
            ScannedFiles++;
            if (IgnoredExtensions.Any(x => path.ToLower().EndsWith(x.ToLower())) || CheckedExtensions.Count != 0 && !CheckedExtensions.Any(x => path.ToLower().EndsWith(x.ToLower())))
            {
                return;
            }
            myWorker.ReportProgress(0);
            CurrentFile = path;
            try
            {
                using (var sr = new StreamReader(path))
                {
                    int lineNumber = 0;
                    while (!sr.EndOfStream)
                    {
                        lineNumber++;
                        string line = sr.ReadLine();
                        if (line != null && line.Length > CharLimit)
                        {
                            ProcessingResult.Add(new ResultEntry()
                            {
                                filePath = path,
                                line = line,
                                lineLength = line.Length,
                                lineNumber = lineNumber
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessingResult.Add(new ResultEntry {filePath = path, line = ex.Message});
            }
            ProcessedFiles++;
        }
    }
}
  

