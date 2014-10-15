using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                        if (tmpSt.Arguments.Count < 3) tmpSt.Arguments.Add(new StringArgument("new {id = Model.ProposalId}"));
                        if (tmpSt.Arguments.Count > 0)
                        {
                            var rOp = tmpSt.Arguments[0].ToString();
                            if (rOp.StartsWith("\"") && rOp.EndsWith("\"") && rOp.Length > 1)
                            {
                                rOp = rOp.Substring(1, rOp.Length - 2);
                                HelperCommentMaker.ReadOperation = rOp;
                            }
                        }
                        if (tmpSt.Arguments.Count > 1)
                        {
                            var controller = tmpSt.Arguments[1].ToString();
                            if (controller.StartsWith("\"") && controller.EndsWith("\"") && controller.Length > 1)
                            {
                                controller = controller.Substring(1, controller.Length - 2);
                                HelperCommentMaker.Controller = controller;
                            }
                        }
                    }

                    tmpSt = (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                            ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.InsertMethod);
                    if (tmpSt != null)
                    {
                        log.LogEntries.Add("\t" + MigrationPrefix + string.Format("Renaming {0} method in {2} to {1}", Consts.InsertMethod, Consts.CreateMethod, Consts.DataBindingMethod));
                        tmpSt.MethodName = Consts.CreateMethod;
                        if (tmpSt.Arguments.Count > 0)
                        {
                            var cOp = tmpSt.Arguments[0].ToString();
                            if (cOp.StartsWith("\"") && cOp.EndsWith("\"") && cOp.Length > 1)
                            {
                                cOp = cOp.Substring(1, cOp.Length - 2);
                                HelperCommentMaker.CreateOperation = cOp;
                            }
                        }
                    }

                    tmpSt = (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                           ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.DeleteMethod);
                    if (tmpSt != null)
                    {
                        log.LogEntries.Add("\t" + MigrationPrefix + string.Format("Renaming {0} method in {2} to {1}", Consts.DeleteMethod, Consts.DestroyMethod, Consts.DataBindingMethod));
                        tmpSt.MethodName = Consts.DestroyMethod;
                        if (tmpSt.Arguments.Count > 0)
                        {
                            var dOp = tmpSt.Arguments[0].ToString();
                            if (dOp.StartsWith("\"") && dOp.EndsWith("\"") && dOp.Length > 1)
                            {
                                dOp = dOp.Substring(1, dOp.Length - 2);
                                HelperCommentMaker.DestroyOperation = dOp;
                            }
                        }
                    }

                    tmpSt = (statement[Consts.DataBindingMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                           ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.UpdateMethod);
                    if (tmpSt != null)
                    {
                        if (tmpSt.Arguments.Count > 0)
                        {
                            var uOp = tmpSt.Arguments[0].ToString();
                            if (uOp.StartsWith("\"") && uOp.EndsWith("\"") && uOp.Length > 1)
                            {
                                uOp = uOp.Substring(1, uOp.Length - 2);
                                HelperCommentMaker.UpdateOperation = uOp;
                            }
                        }
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
                        if (tmpSt.Arguments.Count < 3) tmpSt.Arguments.Add(new StringArgument("new {id = Model.ProposalId}"));
                        if (tmpSt.Arguments.Count > 0)
                        {
                            var rOp = tmpSt.Arguments[0].ToString();
                            if (rOp.StartsWith("\"") && rOp.EndsWith("\"") && rOp.Length > 1)
                            {
                                rOp = rOp.Substring(1, rOp.Length - 2);
                                HelperCommentMaker.ReadOperation = rOp;
                            }
                        }
                        if (tmpSt.Arguments.Count > 1)
                        {
                            var controller = tmpSt.Arguments[1].ToString();
                            if (controller.StartsWith("\"") && controller.EndsWith("\"") && controller.Length > 1)
                            {
                                controller = controller.Substring(1, controller.Length - 2);
                                HelperCommentMaker.Controller = controller;
                            }
                        }
                    }

                    tmpSt = (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                            ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.InsertMethod);
                    if (tmpSt != null)
                    {
                        log.LogEntries.Add("\t" + MigrationPrefix + string.Format("Renaming {0} method in {2} to {1}", Consts.InsertMethod, Consts.CreateMethod, Consts.DataSourceMethod));
                        tmpSt.MethodName = Consts.CreateMethod;
                        if (tmpSt.Arguments.Count > 0)
                        {
                            var cOp = tmpSt.Arguments[0].ToString();
                            if (cOp.StartsWith("\"") && cOp.EndsWith("\"") && cOp.Length > 1)
                            {
                                cOp = cOp.Substring(1, cOp.Length - 2);
                                HelperCommentMaker.CreateOperation = cOp;
                            }
                        }
                    }

                    tmpSt = (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                           ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.DeleteMethod);
                    if (tmpSt != null)
                    {
                        log.LogEntries.Add("\t" + MigrationPrefix + string.Format("Renaming {0} method in {2} to {1}", Consts.DeleteMethod, Consts.DestroyMethod, Consts.DataSourceMethod));
                        tmpSt.MethodName = Consts.DestroyMethod;
                        if (tmpSt.Arguments.Count > 0)
                        {
                            var dOp = tmpSt.Arguments[0].ToString();
                            if (dOp.StartsWith("\"") && dOp.EndsWith("\"") && dOp.Length > 1)
                            {
                                dOp = dOp.Substring(1, dOp.Length - 2);
                                HelperCommentMaker.DestroyOperation = dOp;
                            }
                        }
                    }

                    tmpSt = (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0
                           ].MethodCalls.SingleOrDefault(mc => mc.MethodName == Consts.UpdateMethod);
                    if (tmpSt != null)
                    {
                        if (tmpSt.Arguments.Count > 0)
                        {
                            var uOp = tmpSt.Arguments[0].ToString();
                            if (uOp.StartsWith("\"") && uOp.EndsWith("\"") && uOp.Length > 1)
                            {
                                uOp = uOp.Substring(1, uOp.Length - 2);
                                HelperCommentMaker.UpdateOperation = uOp;
                            }
                        }
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
