using System;
using System.Collections.Generic;
using System.Linq;

namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public class LambdaMethodBody : MethodBodyBase
    {
        public List<IMethodStatement> Statements;
        
        public override string ToString()
        {
            return
                Statements.Select(st => st.ToString())
                    .Aggregate((working, next) => working + ";" + Environment.NewLine + next) + ";";
        }
    }
}
