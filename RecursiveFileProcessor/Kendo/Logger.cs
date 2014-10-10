using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.Kendo
{
    public class Logger
    {
        public List<string> LogEntries { get; private set; }
        public string Name { get { return _prefix; } set { _prefix = value; } }
        private string _prefix;
        private bool _indent;

        public Logger()
        {
            LogEntries = new List<string>();
        }

        public Logger(string prefix)
        {
            LogEntries = new List<string>();
            _prefix = prefix;
        }

        public void StartLog(string message)
        {
            _indent = false;
            Log(message);
            _indent = true;
        }
        
        public void EndLog(string message)
        {
            _indent = false;
            Log(message);
        }

        public void Log(string message)
        {
            LogEntries.Add(string.Format("{0}{1}: {2}",_indent?"\t":string.Empty, _prefix, message));
        }


    }
}
