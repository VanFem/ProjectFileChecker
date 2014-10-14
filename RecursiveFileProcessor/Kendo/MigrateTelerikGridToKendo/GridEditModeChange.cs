using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Migration;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public class GridEditModeChange : IMigrationRule
    {
        private const string MigrationPrefix = "GridEditModeChange";

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
                    || (statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.Count == 0
                    || ((statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][Consts.ModeMethod] == null)
                    || ((statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][Consts.ModeMethod].Arguments.Count == 0)
                    || !((statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][Consts.ModeMethod].Arguments[0] is StringArgument))

                {
                    log.EndLog(string.Format("Did not find a proper `{0}` definition", Consts.ModeMethod));
                    return;
                }

                if (
                    ((statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][
                        Consts.ModeMethod].Arguments[0] as StringArgument).ToString().Trim() ==
                    Consts.GridEditModeInForm)
                {
                    (statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][
                        Consts.ModeMethod].Arguments[0] = new StringArgument(Consts.GridEditModePopUp);
                    log.Log("GridEditMode is InForm; Changed to PopUp");
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
