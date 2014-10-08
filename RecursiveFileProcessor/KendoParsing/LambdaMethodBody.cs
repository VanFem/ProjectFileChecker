using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.KendoParsing
{
    public class LambdaMethodBody : MethodBodyBase
    {
        public List<StatementBase> statements;
        
        public override string ToString()
        {
            return
                statements.Select(st => st.ToString())
                    .Aggregate((working, next) => working + ";" + Environment.NewLine + next) + ";";
        }
    }
}
