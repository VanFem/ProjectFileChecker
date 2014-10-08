using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RecursiveFileProcessor.KendoParsing;

namespace FileProcessor.UnitTests.KendoParsingTests
{
    [TestFixture]
    public class MethodBodyTests
    {
        [Test]
        public void MethodBody_EmptyBody_ReturnsSquigglyBrackets()
        {
            var mb = new MethodBody(false);
            
            Assert.AreEqual("{}", mb.ToString());
        }
        
        [Test]
        public void MethodBody_OneStatementNoLambda_TerminatedBySemicolonAndHasSquigglyBrackets()
        {
            var mb = new MethodBody(false);

            mb.Statements.Add(new StringStatement("code"));
            
            Assert.AreEqual(@"{
code;
}", mb.ToString());
        }

        [Test]
        public void MethodBody_OneStatementLambda_NoSquigglyBracketsOrSemicolons()
        {
            var mb = new MethodBody(true);

            mb.Statements.Add(new StringStatement("code"));

            Assert.AreEqual("code", mb.ToString());
        }

        [Test]
        public void MethodBody_MultipleStatementsLambda_SquigglyBracketsAndSemicolons()
        {
            var mb = new MethodBody(true);

            mb.Statements.Add(new StringStatement("code"));
            mb.Statements.Add(new StringStatement("more code"));
            mb.Statements.Add(new StringStatement("even more code"));

            Assert.AreEqual(@"{
code;
more code;
even more code;
}"
                , mb.ToString());
        }

        [Test]
        public void MethodBody_MultipleStatementsNotLambda_SquigglyBracketsAndSemicolons()
        {
            var mb = new MethodBody(false);

            mb.Statements.Add(new StringStatement("code"));
            mb.Statements.Add(new StringStatement("more code"));
            mb.Statements.Add(new StringStatement("even more code"));

            Assert.AreEqual(@"{
code;
more code;
even more code;
}"
                , mb.ToString());
        }

        [Test]
        public void MethodBody_ComplicatedStatementsNotLambda_ProperStringReturned()
        {
            var mb = new MethodBody(false);

            var st1 = new Statement {Obj = "Console"};
            var m1 = new MethodCall {MethodName = "WriteLine"};
            m1.Arguments.Add(new StringArgument("\"Hello, you {0} world!\""));
            m1.Arguments.Add(new StringArgument("\"beautiful\""));
            st1.MethodCalls.Add(m1);

            var st2 = new Statement { Obj = "new List<int> { 1,2,3,4 }" };
            var m2 = new MethodCall { MethodName = "Select" };
            var la1 = new LambdaTypeArgument();
            la1.LambdaArguments.Add("arg");
            var lst1 = new Statement {Obj = "arg"};
            var lmc1 = new MethodCall {MethodName = "Equals"}; // .Equals(1)
            lmc1.Arguments.Add(new StringArgument("1"));
            var lmc2 = new MethodCall {MethodName = "ToString"};
            lst1.MethodCalls.Add(lmc1);
            lst1.MethodCalls.Add(lmc2);
            la1.LambdaBody.Statements.Add(lst1);
            m2.Arguments.Add(la1);
            st2.MethodCalls.Add(m2);

            var st3 = new Statement();
            var mc3 = new MethodCall {MethodName = "SayHello"};
            st3.MethodCalls.Add(mc3);

            var la2 = new LambdaTypeArgument();
            la2.LambdaArguments.Add("person");
            la2.LambdaArguments.Add("world");
            mc3.Arguments.Add(la2);

            var lst2 = new Statement();
            var lmc4 = new MethodCall {MethodName = "ToEach"};
            lmc4.Arguments.Add(new StringArgument("typeof(person)"));
            var lmc5 = new MethodCall {MethodName = "In"};
            lmc5.Arguments.Add(new StringArgument("World"));
            lst2.MethodCalls.Add(lmc4);
            lst2.MethodCalls.Add(lmc5);
            var lst3 = new Statement(){Obj = "world"};
            var lmc6 = new MethodCall { MethodName = "ReplyTo" };
            lmc6.Arguments.Add(new StringArgument("person"));
            lst3.MethodCalls.Add(lmc6);

            var lst4 = new Statement() { Obj = "person" };
            var lmc7 = new MethodCall { MethodName = "Appreciate" };
            lmc7.Arguments.Add(new StringArgument("world"));
            lst4.MethodCalls.Add(lmc7);


            la2.LambdaBody.Statements.Add(lst2);
            la2.LambdaBody.Statements.Add(lst3);
            la2.LambdaBody.Statements.Add(lst4);
            

            mb.Statements.Add(st1);
            mb.Statements.Add(st2);
            mb.Statements.Add(st3);

            Debug.Write(mb.ToString());
        }
    }
}
