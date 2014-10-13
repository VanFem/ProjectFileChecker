using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Migration;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public class DefaultDataItemMovement : IMigrationRule
    {

        private string _text;
        private int _index;

        private const string MigrationPrefix = "DefaultDataItemMovement";

        public void ApplyTo(Statement statement, Logger log)
        {
            try
            {
                log.Name = MigrationPrefix;
                log.StartLog("Attempting to apply migration...");

                if (statement[Consts.EditableMethod] == null)
                {
                    log.EndLog(string.Format("{0} not found in statement", Consts.EditableMethod));
                    return;
                }



                if (statement[Consts.EditableMethod].Arguments.Count == 0
                    || !(statement[Consts.EditableMethod].Arguments[0] is LambdaTypeArgument)
                    || (statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.All(st => st[Consts.DefaultDataItemMethod] == null)
                    || (statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.First(
                        st => st[Consts.DefaultDataItemMethod] != null)[Consts.DefaultDataItemMethod].Arguments.Count == 0)
                {
                    log.EndLog(string.Format("Arguments for {0} seem invalid", Consts.EditableMethod));
                    return;
                }

                var defaultDataItemDefinition = (statement[Consts.EditableMethod].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements.First(
                        st => st[Consts.DefaultDataItemMethod] != null)[Consts.DefaultDataItemMethod].Arguments[0];

                _index = 0;
                var match = Regex.Match(defaultDataItemDefinition.ToString(), @"new\s+[\w\(\)\<\>\,]+");
                if (match.Success)
                {
                    //TODO
                    //VMName == match.Value.Substring(4)
                }

                if (defaultDataItemDefinition.ToString().Contains('{'))
                {
                    var propertyList = new List<string>();
                    var initializerList = new List<string>();
                    var str =
                            defaultDataItemDefinition.ToString()
                                .Substring(defaultDataItemDefinition.ToString().IndexOf('{'));
                    bool over = false;
                    while (!over)
                    {
                        
                        if (str.Length == 1)
                            throw new MigrationException(
                                string.Format("{0}: Wrong parameters in object initializer", MigrationPrefix), null);
                        str = str.Substring(1).Trim();

                        if (!str.Contains('='))
                            throw new MigrationException(
                                string.Format("{0}: Wrong parameters in object initializer", MigrationPrefix), null);

                        propertyList.Add(str.Substring(0, str.IndexOf('=')));
                        str = str.Substring(str.IndexOf('=') + 1);
                        var closureIndex = GetClosureIndex(str, 0);
                        initializerList.Add(str.Substring(0, closureIndex));
                        if (str[closureIndex] == '}')
                            over = true;
                    }

                    DataSourceCreator.CreateDataSourceIfNoneOrInvalid(statement);

                    for (int i = 0; i < propertyList.Count; i++)
                    {
                        var modelDefinitionCall = new MethodCall(Consts.ModelMethod);
                        var st = new Statement {Obj = Consts.ModelParamName}; // Model(model => [model].Field( ... ) 
                        var fieldMethodCall = new MethodCall(Consts.FieldMethod); // [Field(m => m.FieldName)]
                        var fieldMethodArgs = new LambdaTypeArgument(); // [m] => m.FieldName

                        var fieldSelectCall = new MethodCall(propertyList[i]); // m => m[.FieldName]
                        var fieldSelectStatement = new Statement { Obj = Consts.InnerModelParamName }; // m => [m.FieldName]
                        fieldSelectStatement.MethodCalls.Add(fieldSelectCall);

                        fieldMethodArgs.LambdaArguments = new List<string> { Consts.InnerModelParamName }; // [m] => m.FieldName
                        fieldMethodArgs.LambdaBody = new MethodBody(true) // Field(m => [m.FieldName])
                        {
                            Statements = new List<Statement>
                            {
                                fieldSelectStatement
                            }
                        };

                        fieldMethodCall.Arguments.Add(fieldMethodArgs);





                        


                        var lta = new LambdaTypeArgument();
                        lta.LambdaArguments = new List<string> {Consts.ModelParamName};
                    }




                }
                

                
               


                log.EndLog("Successfully applied migration patch");

            }
            catch (NullReferenceException e)
            {
                throw new MigrationException(MigrationPrefix + ": Null reference exception", e);
            }
        }

        private int GetClosureIndex(string str, int ind)
        {
            var index = ind;
            bool inString = false, inChar = false, nextCharEscaped = false;
            int argDepth = 0;
            char c = str[index];
            while (index < str.Length && ((c != ',' && c != '}') || inString || inChar || (argDepth > 0)))
            {
                c = str[index];
                if (inString || inChar)
                {
                    if (c == '\\' && !nextCharEscaped)
                    {
                        nextCharEscaped = true;
                    }

                    if (c == '\'' && inChar && !nextCharEscaped)
                    {
                        inChar = false;
                    }

                    if (c == '\"' && inString && !nextCharEscaped)
                    {
                        inString = false;
                    }

                    index++;
                    continue;
                }
                if (c == '\'')
                {
                    inChar = true;
                }

                if (c == '"')
                {
                    inString = true;
                }

                if (c == '(')
                {
                    argDepth++;
                }

                if (c == ')')
                {
                    argDepth--;
                    if (argDepth < 0 ) throw new MigrationException(string.Format("{0}: Wrong parameters in object initializer", MigrationPrefix), null);
                }

                index++;
            }

            if (index < str.Length)
            {
                return index;
            }
            throw new MigrationException(string.Format("{0}: Wrong parameters in object initializer", MigrationPrefix), null);
        }
    }
}
