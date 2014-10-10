using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Migration;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public class PagerSettingsMovement : IMigrationRule
    {
        private const string MigrationPrefix = "PagerSettings";

        public void ApplyTo(Statement statement, Logger log)
        {
            try
            {
                log.Name = MigrationPrefix;
                log.StartLog("Attempting to apply migration...");

                if (statement[Consts.Pageable] == null)
                {
                    log.EndLog("No pager settings found in statement");
                    return;
                }

                if (statement[Consts.Pageable].Arguments.Count > 0
                    && statement[Consts.Pageable].Arguments[0] is LambdaTypeArgument
                    && (statement[Consts.Pageable].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.Count > 0
                    &&
                    (statement[Consts.Pageable].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0].MethodCalls
                        .Count > 0)
                {
                    var movingStatements =
                        (statement[Consts.Pageable].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0]
                            .MethodCalls.FindAll(mc => mc.MethodName == Consts.PageSizeMethod || mc.MethodName == Consts.TotalMethod);
                    if (movingStatements.Count != 0)
                    {
                        log.Log("Moving Total and PageSize statements to DataSource.");
                        (statement[Consts.Pageable].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0]
                            .MethodCalls
                            .RemoveAll(
                                mc => mc.MethodName == Consts.PageSizeMethod || mc.MethodName == Consts.TotalMethod);
                        if (
                            (statement[Consts.Pageable].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0]
                                .MethodCalls.Count == 0)
                        {
                            statement[Consts.Pageable].Arguments.RemoveAt(0);
                        }

                        DataSourceCreator.CreateDataSourceIfNoneOrInvalid(statement);
                        (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0]
                            .MethodCalls.AddRange(movingStatements);
                    }
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
