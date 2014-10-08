using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.KendoParsing
{
    public class MethodBody : MethodBodyBase
    {
        public List<StatementBase> Statements;
        public bool IsLambda { get; set; }

        public MethodBody(bool isLambda)
        {
            Statements = new List<StatementBase>();
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
