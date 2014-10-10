using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Migration;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public class DataBindingMovement : IMigrationRule
    {
        public void ApplyTo(Statement statement, Logger log)
        {
            try
            {
                if (statement[Consts.DataBindingMethod] == null)
                {
                    log.LogEntries.Add("DataBindingMovement: DataBinding method doesn't exist in statement");
                    return;
                }

                if (!(statement[Consts.DataBindingMethod].Arguments[0] is LambdaTypeArgument)
                    || (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.Count == 0)
                {
                    log.LogEntries.Add("DataBindingMovement: DataBindingMethod method seems to have invalid parameters");
                    return;
                }
                var bindingDefinitions =
                    (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0].MethodCalls;

                if (statement[Consts.DataSourceMethod] == null)
                {
                    statement.MethodCalls.Add(new MethodCall(Consts.DataSourceMethod));
                }

                if (statement[Consts.DataSourceMethod].Arguments.Count == 0)
                {
                    statement[Consts.DataSourceMethod].Arguments.Add(new LambdaTypeArgument());
                    (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaArguments = new List<string>() { Consts.DataSourceParamName };
                    var dsBody = new MethodBody(true);
                    dsBody.Statements.Add(new Statement { Obj = Consts.DataSourceParamName });
                    (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody = dsBody;

                }

                if (!(statement[Consts.DataSourceMethod].Arguments[0] is LambdaTypeArgument))
                {
                    log.LogEntries.Add("DataBindingMovement: DataSource method seems to have invalid parameters");
                    return;
                }

                (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0].MethodCalls.AddRange(bindingDefinitions);
                statement.MethodCalls.Remove(statement[Consts.DataBindingMethod]);

                log.LogEntries.Add("DataBindingMovement: Successfully applied migration patch");

            }
            catch (NullReferenceException e)
            {
                throw new MigrationException("DataBindingMovement: Null reference exception", e);
            }
        }
    }
}
