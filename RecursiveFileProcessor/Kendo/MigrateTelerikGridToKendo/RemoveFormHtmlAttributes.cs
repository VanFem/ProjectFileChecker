using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Migration;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public class RemoveFormHtmlAttributes : IMigrationRule
    {
        private const string MigrationPrefix = "RemoveFormHtmlAttributes";

        public void ApplyTo(Statement statement, Logger log)
        {
            try
            {
                log.Name = MigrationPrefix;
                log.StartLog("Attempting to apply migration...");

                if (statement[Consts.EditableMethod] == null)
                {
                    log.EndLog(string.Format("{0} not found in statement", Consts.EditableMethod));
                    return;
                }

                if (statement[Consts.EditableMethod].Arguments.Count == 0
                    || !(statement[Consts.EditableMethod].Arguments[0] is LambdaTypeArgument)
                    || (statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements
                        .Count == 0)
                {
                    log.EndLog(string.Format("Arguments for {0} seem invalid", Consts.EditableMethod));
                    return;
                }

                if (
                    (statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][
                        Consts.FormHtmlAttributesMethod] != null)
                {
                    (statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0]
                        .MethodCalls.Remove
                        (
                            (statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[
                                0][
                                    Consts.FormHtmlAttributesMethod]
                        );
                    log.Log(string.Format("Removed {0} method call.", Consts.FormHtmlAttributesMethod));
                }

                log.EndLog("Successfully applied migration patch");

            }
            catch (NullReferenceException e)
            {
                throw new MigrationException(MigrationPrefix + ": Null reference exception", e);
            }
        }
    }
}
