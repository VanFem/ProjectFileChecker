using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public static class DataSourceCreator
    {
        public static void CreateDataSourceIfNoneOrInvalid(Statement st)
        {
            if (st[Consts.DataSourceMethod] == null || st[Consts.DataSourceMethod].Arguments.Count == 0 ||
                !(st[Consts.DataSourceMethod].Arguments[0] is LambdaTypeArgument))
            {
                var mc = new MethodCall(Consts.DataSourceMethod);
                var args = new LambdaTypeArgument();
                args.LambdaArguments.Add(Consts.DataSourceParamName);
                args.LambdaBody.Statements.Add(new Statement() {Obj = Consts.DataSourceParamName});
                mc.Arguments.Add(args);
                if (st[Consts.DataSourceMethod] != null)
                {
                    st.MethodCalls.Remove(st[Consts.DataSourceMethod]);
                }
                st.MethodCalls.Add(mc);
            }
        }
    }
}