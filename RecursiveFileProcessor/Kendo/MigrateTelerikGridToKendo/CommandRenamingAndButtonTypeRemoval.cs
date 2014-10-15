using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Migration;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public class CommandRenamingAndButtonTypeRemoval : IMigrationRule
    {
        private const string MigrationPrefix = "CommandRenaming";

        public void ApplyTo(Statement statement, Logger log)
        {
            try
            {
                log.Name = MigrationPrefix;
                log.StartLog("Attempting to apply migration...");

                if (statement[Consts.ToolBarMethod] == null)
                {
                    log.Log(string.Format("{0} not found in statement", Consts.ToolBarMethod));
                }
                else
                {
                    var toolbarDefinitions = statement.MethodCalls.FindAll(mc => mc.MethodName == Consts.ToolBarMethod);
                    foreach (var tbd in toolbarDefinitions)
                    {
                        if (tbd.Arguments.Count == 0 || !(tbd.Arguments[0] is LambdaTypeArgument)) continue;
                        foreach (var st in (tbd.Arguments[0] as LambdaTypeArgument).LambdaBody.Statements)
                        {
                            foreach (var mc in st.MethodCalls)
                            {
                                //if (mc.MethodName == Consts.UpdateMethod)
                                //{
                                //    log.Log(string.Format("Applying change: method name {0}", mc.MethodName));
                                //    mc.MethodName = Consts.EditMethod;

                                //}
                                if (mc.MethodName == Consts.InsertMethod)
                                {
                                    log.Log(string.Format("Applying change: method name {0}", mc.MethodName));
                                    mc.MethodName = Consts.CreateMethod;
                                }

                                if (mc.MethodName == Consts.DeleteMethod)
                                {
                                    log.Log(string.Format("Applying change: method name {0}", mc.MethodName));
                                    mc.MethodName = Consts.DestroyMethod;
                                }
                            }
                            st.MethodCalls.RemoveAll(mc => mc.MethodName == Consts.ButtonTypeMethod);
                            log.Log(string.Format("Applying change: removing {0} calls", Consts.ButtonTypeMethod));
                        }
                    }
                    log.Log("Applied all toolbar changes.");
                }

                if (statement[Consts.ColumnsMethod] == null)
                {
                    log.Log(string.Format("{0} not found in statement", Consts.ColumnsMethod));
                }
                else
                {
                    var columnDefinitions = statement.MethodCalls.FindAll(mc => mc.MethodName == Consts.ColumnsMethod);
                    foreach (var cd in columnDefinitions)
                    {
                        if (cd.Arguments.Count == 0 || !(cd.Arguments[0] is LambdaTypeArgument)) continue;
                        var lst = (cd.Arguments[0] as LambdaTypeArgument).LambdaBody.Statements;
                        foreach (var st in lst)
                        {
                            if (st[Consts.ClientTemplateMethod] != null)
                            {
                                if (st[Consts.ClientTemplateMethod].Arguments.Count > 0 &&
                                    st[Consts.ClientTemplateMethod].Arguments[0] is StringArgument)
                                {
                                    var str = (st[Consts.ClientTemplateMethod].Arguments[0] as StringArgument).ToString();
                                    st[Consts.ClientTemplateMethod].Arguments[0] = new StringArgument(str.Replace("#", "##").Replace("<##=", "#=").Replace("##>", "#"));
                                }
                            }

                            if (st[Consts.CommandMethod] != null)
                            {
                                var commandDefinitions =
                                    st.MethodCalls.FindAll(mc => mc.MethodName == Consts.CommandMethod);
                                foreach (var comd in commandDefinitions)
                                {
                                    if (comd.Arguments.Count == 0 || !(comd.Arguments[0] is LambdaTypeArgument))
                                        continue;
                                    foreach (var sst in (comd.Arguments[0] as LambdaTypeArgument).LambdaBody.Statements)
                                    {
                                        foreach (var mc in sst.MethodCalls)
                                        {
                                            //if (mc.MethodName == Consts.UpdateMethod)
                                            //{
                                            //    log.Log(string.Format("Applying change: method name {0}", mc.MethodName));
                                            //    mc.MethodName = Consts.EditMethod;
                                            //}
                                            if (mc.MethodName == Consts.InsertMethod)
                                            {
                                                log.Log(string.Format("Applying change: method name {0}", mc.MethodName));
                                                mc.MethodName = Consts.CreateMethod;
                                            }
                                            if (mc.MethodName == Consts.DeleteMethod)
                                            {
                                                log.Log(string.Format("Applying change: method name {0}", mc.MethodName));
                                                mc.MethodName = Consts.DestroyMethod;
                                            }
                                        }
                                        sst.MethodCalls.RemoveAll(mc => mc.MethodName == Consts.ButtonTypeMethod);
                                        log.Log(string.Format("Applying change: removing {0} calls", Consts.ButtonTypeMethod));
                                    }
                                }
                            }

                        }
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
