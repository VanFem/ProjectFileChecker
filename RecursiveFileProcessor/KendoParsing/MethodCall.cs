using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.KendoParsing
{
    public class MethodCall : MethodCallBase
    {
        public string MethodName { get; set; }
        public List<ArgumentBase> Arguments { get; private set; }

        public MethodCall()
        {
            Arguments = new List<ArgumentBase>();
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(MethodName)) return string.Empty;

            var builder = new StringBuilder(MethodName);
            builder.Append("(");
            if (Arguments.Count != 0)
            {
                builder.Append(
                    Arguments.Select(arg => arg.ToString()).Aggregate((working, next) => working + ", " + next));
            }
            builder.Append(")");
            return builder.ToString();
        }
    }
}
