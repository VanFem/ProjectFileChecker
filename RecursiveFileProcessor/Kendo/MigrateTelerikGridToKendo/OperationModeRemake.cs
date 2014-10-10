using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Migration;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public class OperationModeRemake : IMigrationRule
    {
        private const string MigrationPrefix = "OperationModeRemake: ";

        public void ApplyTo(Statement statement, Logger log)
        {
            try
            {
                log.LogEntries.Add(MigrationPrefix + "Attempting to apply migration...");
                if (statement[Consts.DataBindingMethod] == null && statement[Consts.DataSourceMethod] == null)
                {
                    log.LogEntries.Add(MigrationPrefix + "DataBinding and DataSource methods don't exist in the statement.");
                    return;
                }

                

                MethodCall operationModeCall = null;
                Statement addToStatement = null;

                if (statement[Consts.DataBindingMethod] != null &&
                    statement[Consts.DataBindingMethod].Arguments[0] is LambdaTypeArgument &&
                    (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.Count > 0 &&
                    (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][Consts.OperationModeMethod] != null)
                {
                    operationModeCall =
                        (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                            ][Consts.OperationModeMethod];
                    (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0]
                        .MethodCalls.Remove(operationModeCall);
                    log.LogEntries.Add("\t"+MigrationPrefix + string.Format("Found operation mode in {0}", Consts.DataBindingMethod));
                }

                if (statement[Consts.DataSourceMethod] != null &&
                    statement[Consts.DataSourceMethod].Arguments[0] is LambdaTypeArgument &&
                    (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.Count >
                    0)
                {
                    if (
                        (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0]
                            [Consts.OperationModeMethod] != null)
                    {

                        operationModeCall =
                            (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody
                                .Statements[0
                                ][Consts.OperationModeMethod];
                        (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0]
                            .MethodCalls.Remove(operationModeCall);
                        log.LogEntries.Add("\t" + MigrationPrefix +
                                           string.Format(
                                               "Found operation mode in {0} (may overwrite any previous found)",
                                               Consts.DataSourceMethod));
                    }
                }
                else
                {
                    if (statement[Consts.DataSourceMethod] == null)
                    {
                        statement.MethodCalls.Add(new MethodCall(Consts.DataSourceMethod));
                    }

                    if (statement[Consts.DataSourceMethod].Arguments.Count == 0)
                    {
                        statement[Consts.DataSourceMethod].Arguments.Add(new LambdaTypeArgument());
                        (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaArguments =
                            new List<string>() {Consts.DataSourceParamName};
                        var dsBody = new MethodBody(true);
                        dsBody.Statements.Add(new Statement {Obj = Consts.DataSourceParamName});
                        (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody = dsBody;
                    }
                }

                addToStatement =
                        (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0];

                if (operationModeCall == null)
                {
                    log.LogEntries.Add(MigrationPrefix + "Operation mode not found in statement. Migration skipped.");
                    return;
                }

                if (operationModeCall.Arguments.Count == 0 || !(operationModeCall.Arguments[0] is StringArgument))
                {
                    log.LogEntries.Add(MigrationPrefix + string.Format("Operation mode arguments appear invalid: {0}", operationModeCall));
                    return;
                }

                var mc = new MethodCall(Consts.ServerOperationMethod);
                if ((operationModeCall.Arguments[0] as StringArgument).ArgName == Consts.ClientOperationModeEnumName)
                {
                    mc.Arguments.Add(new StringArgument(Consts.FalseParameter));
                }
                else if ((operationModeCall.Arguments[0] as StringArgument).ArgName == Consts.ServerOperationModeEnumName)
                {
                    mc.Arguments.Add(new StringArgument(Consts.TrueParameter));
                }
                else
                {
                    log.LogEntries.Add("\t" + MigrationPrefix + string.Format("Could not determine type of operation: {0}",(operationModeCall.Arguments[0] as StringArgument).ArgName));
                    return;
                }

                addToStatement.MethodCalls.Add(mc);
                log.LogEntries.Add(MigrationPrefix + "Successfully applied migration patch");

            }
            catch (NullReferenceException e)
            {
                throw new MigrationException(MigrationPrefix + "Null reference exception", e);
            }
        }
    }
}
