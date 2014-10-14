using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public class Statement : IMethodStatement
    {
        private const string RazorCommentFormat = "@*{0}*@";
        private const string CCommentFormat = "/*{0}*/";
        private const string BracketFormat = "({0})";


        public string Obj { get; set; }
        public bool SurroundInBrackets { get; set; }

        public MethodCall this[string val]
        {
            get { return MethodCalls.FirstOrDefault(m => m.MethodName == val); }
        }

        public List<MethodCall> MethodCalls { get; private set; }
        public int Indent { get; set; }
        public string Comment { get; set; }
        public bool IsRazorComment { get; set; }

        public Statement()
        {
            MethodCalls = new List<MethodCall>();
        }

        public override string ToString()
        {
            var strBuilder = string.IsNullOrEmpty(Obj) ? new StringBuilder() : new StringBuilder(Obj);
            if (MethodCalls.Count == 0) return strBuilder.ToString();

            MethodCalls.ForEach(mc => mc.Indent = Indent + 1);
            if (!string.IsNullOrEmpty(Obj)) strBuilder.Append('.');
            strBuilder.Append(MethodCalls[0]);
            if (MethodCalls.Count > 1)
            {
                int startRange = 1;
                if (!string.IsNullOrEmpty(Obj))
                {
                    strBuilder.Append(Environment.NewLine + Indenter.GetIndented(Indent + 1, "."));
                }
                else
                {
                    strBuilder.Append(".");
                    strBuilder.Append(MethodCalls[1]);
                    if (MethodCalls.Count > 2)
                    {
                        strBuilder.Append(Environment.NewLine + Indenter.GetIndented(Indent + 1, "."));
                        startRange++;
                    }
                    else
                    {
                        if (SurroundInBrackets)
                            strBuilder = new StringBuilder(string.Format(BracketFormat, strBuilder));


                        if (string.IsNullOrEmpty(Comment)) return strBuilder.ToString();

                        strBuilder.Append(Environment.NewLine);
                        strBuilder.AppendFormat(IsRazorComment ? RazorCommentFormat : CCommentFormat, Comment);
                        return strBuilder.ToString();
                    }
                }
                strBuilder.Append(
                    MethodCalls.GetRange(startRange, MethodCalls.Count - startRange).Select(mc => mc.ToString())
                        .Aggregate(
                            (working, next) =>
                                working + Environment.NewLine + Indenter.GetIndented(Indent + 1, "." + next)));
            }

            if (SurroundInBrackets) 
                strBuilder = new StringBuilder(string.Format(BracketFormat, strBuilder));
                

            if (string.IsNullOrEmpty(Comment)) return strBuilder.ToString();

            strBuilder.Append(Environment.NewLine);
            strBuilder.AppendFormat(IsRazorComment ? RazorCommentFormat : CCommentFormat, Comment);
            return strBuilder.ToString();
        }
    }
}
