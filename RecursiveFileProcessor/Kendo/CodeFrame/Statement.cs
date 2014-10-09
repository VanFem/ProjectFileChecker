using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public class Statement : IMethodStatement
    {
        public string Obj { get; set; }

        public MethodCall this[string val]
        {
            get { return MethodCalls.FirstOrDefault(m => m.MethodName == val); }
        }
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
