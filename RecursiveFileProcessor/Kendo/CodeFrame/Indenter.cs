using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public static class Indenter
    {
        public static string GetIndented(int shift, string text)
        {
            var strBuilder = new StringBuilder();
            for (int i = 0; i < shift; i++)
            {
                strBuilder.Append('\t');
            }

            return string.Format("{0}{1}", strBuilder, text);
        }
    }
}
