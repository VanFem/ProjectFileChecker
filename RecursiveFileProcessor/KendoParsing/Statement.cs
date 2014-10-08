using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.KendoParsing
{
    public class Statement : StatementBase
    {
        public string Obj { get; set; }
        public List<MethodCall> MethodCalls { get; private set; }

        public Statement()
        {
            MethodCalls = new List<MethodCall>();
        }

        public override string ToString()
        {
            var strBuilder = string.IsNullOrEmpty(Obj) ? new StringBuilder() : new StringBuilder(Obj);
            
            if (MethodCalls.Count == 0) return strBuilder.ToString();
            
            if (!string.IsNullOrEmpty(Obj)) strBuilder.Append(".");
            strBuilder.Append(MethodCalls.Select(mc => mc.ToString()).Aggregate((working, next) => working + "." + next));
            
            return strBuilder.ToString();
        }
    }
}
