using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.KendoParsing
{
    public class StringArgument : ArgumentBase
    {
        public string ArgName { get; set; }

        public StringArgument(string argName)
        {
            ArgName = argName;
        }

        public override string ToString()
        {
            return ArgName;
        }
    }
}
