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
    public class StatementTest
    {
        [Test]
        public void Statement_Empty_ReturnsEmptyString()
        {
            var st = new Statement();

            Assert.AreEqual(string.Empty, st.ToString());
        }

        [Test]
        public void Statement_OnlyObject_ReturnsObjectString()
        {
            var st = new Statement();
            st.Obj = "myAmazingObject";

            Assert.AreEqual("myAmazingObject", st.ToString());
        }

        [Test]
        public void Statement_OnlyMethod_ReturnsMethodCallString()
        {
            var st = new Statement();
            var mc = new MethodCall {MethodName = "AmazingMethod"};
            mc.Arguments.Add(new StringArgument("awesomeArgument"));
            st.MethodCalls.Add(mc);

            Assert.AreEqual("AmazingMethod(awesomeArgument)", st.ToString());
        }

        [Test]
        public void Statement_MultipleMethods_ReturnsMethodCallChainString()
        {
            var st = new Statement();
            var mc = new MethodCall { MethodName = "AmazingMethod" };
            mc.Arguments.Add(new StringArgument("awesomeArgument"));
            var mc2 = new MethodCall { MethodName = "AnotherMethod" };
            var mc3 = new MethodCall { MethodName = "YetAnotherMethod" };
            mc3.Arguments.Add(new StringArgument("amazingArgument"));
            mc3.Arguments.Add(new StringArgument("coolAmazingArgument"));
            mc3.Arguments.Add(new StringArgument("notSoAmazingArgument"));

            st.MethodCalls.Add(mc);
            st.MethodCalls.Add(mc2);
            st.MethodCalls.Add(mc3);

            Assert.AreEqual("AmazingMethod(awesomeArgument).AnotherMethod().YetAnotherMethod(amazingArgument, coolAmazingArgument, notSoAmazingArgument)", 
                st.ToString());
        }


    }
}
