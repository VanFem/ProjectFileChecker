using System;
using System.Collections.Generic;
using System.Linq;

namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public class MethodBody : MethodBodyBase
    {

        public Statement this[string val]
        {
            get { return Statements.FirstOrDefault(st => st.Obj == val); }
        }

        public List<Statement> Statements;
        public bool IsLambda { get; set; }

        public MethodBody(bool isLambda)
        {
            Statements = new List<Statement>();
            IsLambda = isLambda;
        }

        public override string ToString()
        {
            if (Statements.Count == 0) return "{}";
            var body = Statements.Select(st => st.ToString())
                .Aggregate((working, next) => working + ";" + Environment.NewLine + next);
            if (!IsLambda || Statements.Count > 1)
            {
                body = "{"+Environment.NewLine + body + ";"+Environment.NewLine+"}";
            }

            return body;
        }
    }
}
