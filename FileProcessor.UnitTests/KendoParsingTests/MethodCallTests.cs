using NUnit.Framework;
using RecursiveFileProcessor.Kendo.CodeFrame;

namespace FileProcessor.UnitTests.KendoParsingTests
{
    [TestFixture]
    public class MethodCallTests
    {
        [Test]
        public void MethodCall_EmptyMethodCall_ReturnsEmptyString()
        {
            var mc = new MethodCall();

            Assert.AreEqual(string.Empty, mc.ToString());
        }

        [Test]
        public void MethodCall_MethodCallWithNoArguments_ProperString()
        {
            var mc = new MethodCall();

            mc.MethodName = "AwesomeMethod";

            Assert.AreEqual("AwesomeMethod()", mc.ToString());
        }

        [Test]
        public void MethodCall_MethodCallWithArguments_ProperString()
        {
            var mc = new MethodCall();

            mc.MethodName = "AwesomeMethod";
            mc.Arguments.Add(new StringArgument("arg1"));
            mc.Arguments.Add(new StringArgument("arg2"));

            Assert.AreEqual("AwesomeMethod(arg1, arg2)", mc.ToString());
        }
    }
}
