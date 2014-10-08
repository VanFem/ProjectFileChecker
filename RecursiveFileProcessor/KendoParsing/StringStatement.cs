using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.KendoParsing
{
    public class StringStatement : StatementBase
    {
        public string Code { get; set; }
        
        public StringStatement(string code)
        {
            Code = code;
        }
        
        public override string ToString()
        {
            return Code;
        }
    }
}
