using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursiveFileProcessor
{
    public partial class ResultDisplay : Form
    {
        private FileProcessor _fileProcessor;

        public ResultDisplay(FileProcessor fileProcessor)
        {
            InitializeComponent();
            _fileProcessor = fileProcessor;
            resultEntryBindingSource.DataSource = _fileProcessor.ProcessingResult;
        }
    }
}
