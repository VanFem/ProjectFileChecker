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

        public Logger()
        {
            LogEntries = new List<string>();
        }
    }
}
