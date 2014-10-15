using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
                    foreach (
                        var mst in (statement[Consts.Pageable].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements
                        )
                    {
                        var movingStatements =
                            mst.MethodCalls.FindAll(
                                mc => mc.MethodName == Consts.PageSizeMethod || mc.MethodName == Consts.TotalMethod);

                        if (movingStatements.Count != 0)
                        {
                            log.Log("Moving Total and PageSize statements to DataSource.");
                            mst.MethodCalls
                                .RemoveAll(
                                    mc => mc.MethodName == Consts.PageSizeMethod || mc.MethodName == Consts.TotalMethod);

                            DataSourceCreator.CreateDataSourceIfNoneOrInvalid(statement);
                            (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody
                                .Statements[0]
                                .MethodCalls.AddRange(movingStatements);
                        }
                    }
                    (statement[Consts.Pageable].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.RemoveAll(
                        st => st.MethodCalls.Count == 0);
                    if ((statement[Consts.Pageable].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.Count == 0)
                    {
                       statement[Consts.Pageable].Arguments.RemoveAt(0); 
                    }
                }


                if (statement[Consts.Pageable] != null)
                {
                    Statement confStatement;
                    if (statement[Consts.Pageable].Arguments.Count == 0 ||
                        !(statement[Consts.Pageable].Arguments[0] is LambdaTypeArgument))
                    {
                        var lta = new LambdaTypeArgument();
                        lta.LambdaArguments.Add(Consts.PagerParamName);
                        lta.LambdaBody.Statements.Add(new Statement() { Obj = Consts.PagerParamName });
                        statement[Consts.Pageable].Arguments.Add(lta);
                        confStatement = lta.LambdaBody.Statements[0];
                    }
                    else
                    {
                        confStatement =
                            (statement[Consts.Pageable].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0];
                    }

                    var mc = new MethodCall(Consts.RefreshMethod);
                    mc.Arguments.Add(new StringArgument(Consts.TrueParameter));
                    confStatement.MethodCalls.Add(mc);
                }
                else
                {
                    var pageableCall = new MethodCall(Consts.Pageable);
                    var lta = new LambdaTypeArgument();
                    lta.LambdaArguments.Add(Consts.PagerParamName);
                    lta.LambdaBody.Statements.Add(new Statement() { Obj = Consts.PagerParamName });
                    statement[Consts.Pageable].Arguments.Add(lta);
                    pageableCall.Arguments.Add(lta);
                    var confStatement = lta.LambdaBody.Statements[0];
                    var mc = new MethodCall(Consts.RefreshMethod);
                    mc.Arguments.Add(new StringArgument(Consts.TrueParameter));
                    confStatement.MethodCalls.Add(mc);
                    statement.MethodCalls.Add(pageableCall);
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
