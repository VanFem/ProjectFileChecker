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

        public override string ToString()
        {
            var strBuilder = new StringBuilder(Obj);
            foreach (var mc in MethodCalls)
            {
                strBuilder.Append(".");
                strBuilder.Append(mc.ToString());
            }
            return strBuilder.ToString();
        }
    }
}
