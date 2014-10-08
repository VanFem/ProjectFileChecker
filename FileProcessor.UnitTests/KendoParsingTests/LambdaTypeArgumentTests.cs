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
    public class LambdaTypeArgumentTests
    {
        [Test]
        public void LambdaTypeArgument_EmptyArgument_ReturnsBracketsAndEmptyBody()
        {
            var lta = new LambdaTypeArgument();
            
            Assert.AreEqual("() => {}", lta.ToString());
        }

        [Test]
        public void LambdaTypeArgument_OneArgument_HasNoBracketsAround()
        {
            var lta = new LambdaTypeArgument();

            lta.LambdaArguments.Add("arg");

            Assert.AreEqual("arg => {}", lta.ToString());
        }

        [Test]
        public void LambdaTypeArgument_TwoArguments_HaveBracketsAround()
        {
            var lta = new LambdaTypeArgument();

            lta.LambdaArguments.Add("arg");
            lta.LambdaArguments.Add("arg2");

            Assert.AreEqual("(arg, arg2) => {}", lta.ToString());
        }

        [Test]
        public void LambdaTypeArgument_OneStatement_HasNoSquigglyBrackets()
        {
            var lta = new LambdaTypeArgument();
            
            lta.LambdaBody.Statements.Add(new StringStatement("code"));

            Assert.AreEqual("() => code", lta.ToString());
        }

        [Test]
        public void LambdaTypeArgument_TwoStatements_HaveSquigglyBracketsAndTerminatedBySemicolons()
        {
            var lta = new LambdaTypeArgument();

            lta.LambdaBody.Statements.Add(new StringStatement("code"));
            lta.LambdaBody.Statements.Add(new StringStatement("more code"));

            Assert.AreEqual(@"() => {
code;
more code;
}"
                , lta.ToString());
        }

        [Test]
        public void LambdaTypeArgument_MultipleStatementsAndARguments_AppropriateResult()
        {
            var lta = new LambdaTypeArgument();

            lta.LambdaBody.Statements.Add(new StringStatement("code"));
            lta.LambdaBody.Statements.Add(new StringStatement("more code"));
            lta.LambdaBody.Statements.Add(new StringStatement("even more code"));
            lta.LambdaArguments.Add("arg1");
            lta.LambdaArguments.Add("arg2");
            lta.LambdaArguments.Add("arg3");

            Assert.AreEqual(@"(arg1, arg2, arg3) => {
code;
more code;
even more code;
}"
                , lta.ToString());

        }
    }
}
