using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Migration;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    // Select -> Read
    // Insert -> Create
    // Delete -> Destroy
    public class CRUDRename :IMigrationRule
    {
        private const string MigrationPrefix = "CRUDRename: ";

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

                if (statement[Consts.DataBindingMethod] != null &&
                    statement[Consts.DataBindingMethod].Arguments[0] is LambdaTypeArgument &&
                    (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.Count > 0)
                {
                    var tmpSt =
                        (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                            ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.SelectMethod);
                    if (tmpSt != null)
                    {
                        log.LogEntries.Add("\t"+MigrationPrefix + string.Format("Renaming {0} method in {2} to {1}", Consts.SelectMethod, Consts.ReadMethod, Consts.DataBindingMethod));
                        tmpSt.MethodName = Consts.ReadMethod;
                    }

                    tmpSt = (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                            ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.InsertMethod);
                    if (tmpSt != null)
                    {
                        log.LogEntries.Add("\t" + MigrationPrefix + string.Format("Renaming {0} method in {2} to {1}", Consts.InsertMethod, Consts.CreateMethod, Consts.DataBindingMethod));
                        tmpSt.MethodName = Consts.CreateMethod;
                    }

                    tmpSt = (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                           ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.DeleteMethod);
                    if (tmpSt != null)
                    {
                        log.LogEntries.Add("\t" + MigrationPrefix + string.Format("Renaming {0} method in {2} to {1}", Consts.DeleteMethod, Consts.DestroyMethod, Consts.DataBindingMethod));
                        tmpSt.MethodName = Consts.DestroyMethod;
                    }
                }

                if (statement[Consts.DataSourceMethod] != null &&
                    statement[Consts.DataSourceMethod].Arguments[0] is LambdaTypeArgument &&
                    (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.Count >
                    0)
                {
                    var tmpSt =
                        (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                            ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.SelectMethod);
                    if (tmpSt != null)
                    {
                        log.LogEntries.Add("\t" + MigrationPrefix + string.Format("Renaming {0} method in {2} to {1}", Consts.SelectMethod, Consts.ReadMethod, Consts.DataSourceMethod));
                        tmpSt.MethodName = Consts.ReadMethod;
                    }

                    tmpSt = (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                            ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.InsertMethod);
                    if (tmpSt != null)
                    {
                        log.LogEntries.Add("\t" + MigrationPrefix + string.Format("Renaming {0} method in {2} to {1}", Consts.InsertMethod, Consts.CreateMethod, Consts.DataSourceMethod));
                        tmpSt.MethodName = Consts.CreateMethod;
                    }

                    tmpSt = (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                           ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.DeleteMethod);
                    if (tmpSt != null)
                    {
                        log.LogEntries.Add("\t" + MigrationPrefix + string.Format("Renaming {0} method in {2} to {1}", Consts.DeleteMethod, Consts.DestroyMethod, Consts.DataSourceMethod));
                        tmpSt.MethodName = Consts.DestroyMethod;
                    }
                }
                log.LogEntries.Add(MigrationPrefix + "Successfully applied migration patch");

            }
            catch (NullReferenceException e)
            {
                throw new MigrationException(MigrationPrefix + "Null reference exception", e);
            }
        }
    }
}
