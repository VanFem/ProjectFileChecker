using System.Collections.Generic;
using System.Linq;

namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public class LambdaTypeArgument : IArgumentBase
    {
        private const string LambdaFormatString = "{0} => {1}";
        public List<string> LambdaArguments { get; set; }
        public MethodBody LambdaBody { get; set; }
        public int Indent { get; set; }

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
            LambdaBody.Indent = Indent;
            return string.Format(LambdaFormatString, args, LambdaBody);
        }
    }
}
