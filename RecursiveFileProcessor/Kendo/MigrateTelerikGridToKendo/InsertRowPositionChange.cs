using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Migration;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public class InsertRowPositionChange : IMigrationRule
    {
        private const string MigrationPrefix = "InsertRowPositionChange";

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
                    || ((statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][Consts.InsertRowPositionMethod] == null))
                {
                    log.EndLog(string.Format("Did not find an `{0}` definition", Consts.InsertRowPositionMethod));
                    return;
                }

                (statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][
                    Consts.InsertRowPositionMethod].MethodName = Consts.CreateAtMethod;


                log.Log(string.Format("`{0}` renamed to `{1}`", Consts.InsertRowPositionMethod, Consts.CreateAtMethod));

                log.EndLog("Successfully applied migration patch");

            }
            catch (NullReferenceException e)
            {
                throw new MigrationException(MigrationPrefix + ": Null reference exception", e);
            }
        }
    }
}
