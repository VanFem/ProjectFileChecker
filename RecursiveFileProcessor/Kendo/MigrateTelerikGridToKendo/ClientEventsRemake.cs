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
    public class ClientEventsRemake :IMigrationRule
    {
        private const string MigrationPrefix = "ClientEventsRemake";

        public void ApplyTo(Statement statement, Logger log)
        {
            try
            {
                log.Name = MigrationPrefix;
                log.StartLog("Attempting to apply migration...");

                if (statement[Consts.ClientEventsMethod] == null)
                {
                    log.EndLog(string.Format("{0} not found in statement", Consts.ClientEventsMethod));
                    return;
                }

                if (statement[Consts.ClientEventsMethod].Arguments.Count == 0
                    || !(statement[Consts.ClientEventsMethod].Arguments[0] is LambdaTypeArgument)
                    ||
                    (statement[Consts.ClientEventsMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements
                        .Count == 0)
                {
                    log.EndLog(string.Format("Arguments for {0} seem invalid", Consts.ClientEventsMethod));
                    return;
                }

                var eventDefinitions =
                    (statement[Consts.ClientEventsMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[
                        0];

                log.Log("Found event definitions");


                var errorMethod = eventDefinitions.MethodCalls.Find(mc => mc.MethodName == Consts.OnErrorMethod);
                if (errorMethod == null)
                {
                    log.Log("No OnError event was found");
                }
                else
                {
                    log.Log("Found OnError event, moving to {0}");
                    var eventsCall = new MethodCall(Consts.EventsMethod);
                    eventsCall.Arguments.Add(new LambdaTypeArgument
                    {
                        LambdaArguments = new List<string> {Consts.EventsParamName},
                        LambdaBody = new MethodBody(true)
                        {
                            Statements = new List<Statement> {new Statement {Obj = Consts.EventsParamName}}
                        }
                    });
                    (eventsCall.Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0].MethodCalls.Add(
                        errorMethod);
                    DataSourceCreator.CreateDataSourceIfNoneOrInvalid(statement);
                    errorMethod.MethodName = Consts.ErrorMethod;
                    (statement[Consts.DataSourceMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0]
                        .MethodCalls.Add(eventsCall);
                    eventDefinitions.MethodCalls.Remove(errorMethod);
                }



                eventDefinitions.MethodCalls.ForEach(mc =>
                {
                    if (mc.MethodName.StartsWith("On") && mc.MethodName.Length > 2)
                    {
                        log.Log(string.Format("Renaming {0} into {1}", mc.MethodName, mc.MethodName.Substring(2)));
                        mc.MethodName = mc.MethodName.Substring(2);
                    }
                });

                
                if (eventDefinitions.MethodCalls.Count == 0)
                {
                    statement.MethodCalls.Remove(statement[Consts.ClientEventsMethod]);
                    log.Log(string.Format("Removing empty `{0}` method", Consts.ClientEventsMethod));
                }
                else
                {
                    statement[Consts.ClientEventsMethod].MethodName = Consts.EventsMethod;
                    log.Log(string.Format("Renaming {0} into {1}", Consts.ClientEventsMethod, Consts.EventsMethod));
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
