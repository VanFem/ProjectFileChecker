using System;
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
        public int Indent { get; set; }

        public Statement()
        {
            MethodCalls = new List<MethodCall>();
        }

        public override string ToString()
        {
            var strBuilder = string.IsNullOrEmpty(Obj) ? new StringBuilder() : new StringBuilder(Obj);
            
            if (MethodCalls.Count == 0) return strBuilder.ToString();

            MethodCalls.ForEach(mc => mc.Indent = Indent + 1);
            strBuilder.Append("." + MethodCalls[0]);
            if (MethodCalls.Count > 1)
            {
                if (!string.IsNullOrEmpty(Obj))
                    strBuilder.Append(Environment.NewLine + Indenter.GetIndented(Indent + 1, "."));
                strBuilder.Append(
                    MethodCalls.GetRange(1, MethodCalls.Count-1).Select(mc => mc.ToString())
                        .Aggregate(
                            (working, next) =>
                                working + Environment.NewLine + Indenter.GetIndented(Indent + 1, "." + next)));
            }

            return strBuilder.ToString();
        }
    }
}
