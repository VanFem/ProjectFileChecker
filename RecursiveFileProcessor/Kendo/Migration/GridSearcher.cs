using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.Kendo.Migration
{
    public static class GridSearcher
    {
        public static List<string> GetGridList(string text)
        {
            var results = new List<string>();
            var regex = new Regex(@"@\(\s*Html\s*\.\s*Kendo\s*\(\s*\)\s*\.\s*Grid");
            var match = regex.Match(text);
            while (match.Success)
            {
                int pos = match.Index+2;
                int bracketDepth = 1;
                
                while (bracketDepth > 0 && pos < text.Length)
                {
                    if (text[pos] == '(') bracketDepth++;
                    if (text[pos] == ')') bracketDepth--;
                    pos++;
                }
                if (pos == text.Length && bracketDepth > 0)
                {
                    throw new Exception("Cannot parse grid.");
                }
                results.Add(text.Substring(match.Index, pos - match.Index));
                match = match.NextMatch();
            }

            return results;
        }
    }
}
