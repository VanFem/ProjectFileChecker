using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Migration;

// ReSharper disable PossibleNullReferenceException

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public class DataKeysMovement : IMigrationRule
    {
        public void ApplyTo(Statement statement, Logger log)
        {
            try
            {
                if (statement[Consts.DataKeysMethod] == null)
                {
                    log.LogEntries.Add("DataKeysMovement: DataKeys method doesn't exist in statement");
                    return;
                }

                if (!(statement[Consts.DataKeysMethod].Arguments[0] is LambdaTypeArgument)
                    || (statement[Consts.DataKeysMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.Count == 0
                    || (statement[Consts.DataKeysMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][Consts.AddMethod] == null
                    || (statement[Consts.DataKeysMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][Consts.AddMethod].Arguments.Count == 0)
                {
                    log.LogEntries.Add("DataKeysMovement: DataKeys method seems to have invalid parameters");
                    return;
                }
                var keysDefinitionArgument = (statement[Consts.DataKeysMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0][Consts.AddMethod].Arguments[0];

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
                    log.LogEntries.Add("DataKeysMovement: DataSource method seems to have invalid parameters");
                    return;
                }

                var modelIdSetup = new MethodCall(Consts.ModelMethod);
                var modelIdLambda = new LambdaTypeArgument();
                var modelIdLambdaMethod = new MethodBody(true);

                var modelIdLambdaMethodStatementMethodCall = new MethodCall(Consts.IdMethod);
                modelIdLambdaMethodStatementMethodCall.Arguments.Add(
                    keysDefinitionArgument);

                modelIdLambdaMethod.Statements.Add(new Statement {Obj = Consts.ModelParamName});
                modelIdLambdaMethod.Statements[0].MethodCalls.Add(modelIdLambdaMethodStatementMethodCall);
                modelIdLambda.LambdaArguments.Add(Consts.ModelParamName);
                modelIdLambda.LambdaBody = modelIdLambdaMethod;

                modelIdSetup.Arguments.Add(modelIdLambda);
                
                (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0].MethodCalls.Add(modelIdSetup);
                statement.MethodCalls.Remove(statement[Consts.DataKeysMethod]);

                log.LogEntries.Add("DataKeysMovement: Successfully applied migration patch");

            }
            catch (NullReferenceException e)
            {
                throw new MigrationException("DataKeysMovement: Null reference exception", e);
            }


        }
    }
}

// ReSharper restore PossibleNullReferenceException