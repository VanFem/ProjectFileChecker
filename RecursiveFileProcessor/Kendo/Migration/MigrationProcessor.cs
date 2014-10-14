using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo;

namespace RecursiveFileProcessor.Kendo.Migration
{
    public class MigrationProcessor
    {
        public Migration AppliedMigration;
        private static string newGridSurround = "@({0})";

        public string ProcessFile(string filePath)
        {
            var text = new StringBuilder(File.ReadAllText(filePath));
            var grids = GridSearcher.GetGridList(text.ToString());
            var helperComments = new List<string>();
            var newGrids = new List<string>();
            var parser = new Parser.CodeParser();

            var result = new StringBuilder();

            foreach (var grid in grids)
            {
                parser.ParseSingleStatement(grid.Substring(2, grid.Length - 2));
                parser.Code.IsLambda = true;
                AppliedMigration.ApplyMigration(parser.Code.Statements[0]);
                helperComments.Add(HelperCommentMaker.GetHelperComment());

                newGrids.Add(string.Format(newGridSurround,parser.Code));

                result.Append(
                    AppliedMigration.MigrationLog.LogEntries.Aggregate(
                        (working, next) => working + Environment.NewLine + next));
            }
            
            if (grids.Count > 0) result.Append(Environment.NewLine);

            for (int i = 0; i < grids.Count; i++)
            {
                text = text.Replace(grids[i], newGrids[i]);
            }

            foreach (var hs in helperComments)
            {
                text.Append(hs);
            }
            
            File.WriteAllText(filePath, text.ToString());
            return result.ToString();
        }

        
    }
}
