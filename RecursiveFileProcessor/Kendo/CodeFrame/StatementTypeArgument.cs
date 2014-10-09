using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public class StatementTypeArgument : IArgumentBase
    {
        public bool IsNew { get; set; }

        public Statement ArgumentStatement { get; set; }

        public StatementTypeArgument(Statement st)
        {
            ArgumentStatement = st;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            if (IsNew)
            {
                strBuilder.Insert(0, "new ");
            }
            strBuilder.Append(ArgumentStatement);
            return strBuilder.ToString();
            
        }
    }
}
