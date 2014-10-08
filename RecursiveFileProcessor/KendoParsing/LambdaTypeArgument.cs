using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.KendoParsing
{
    public class LambdaTypeArgument : ArgumentBase
    {
        private const string LambdaFormatString = "{0} => {1}";
        public List<string> LambdaArguments { get; private set; }
        public MethodBody LambdaBody { get; set; }

        public LambdaTypeArgument()
        {
            LambdaArguments = new List<string>();
            LambdaBody = new MethodBody(true);
        }

        public override string ToString()
        {
            string args;
            if (LambdaArguments.Count == 1)
            {
                args = LambdaArguments[0];
            }
            else if (LambdaArguments.Count == 0)
            {
                args = "()";
            } 
            else
            {
                args = "(" + LambdaArguments.Aggregate((working, next) => working + ", " + next) + ")";
            }
            return string.Format(LambdaFormatString, args, LambdaBody);
        }
    }
}
