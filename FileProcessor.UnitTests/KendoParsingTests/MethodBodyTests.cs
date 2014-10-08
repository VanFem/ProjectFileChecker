using System;
using System.Collections.Generic;
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
    }
}
