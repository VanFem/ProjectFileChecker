using NUnit.Framework;
using RecursiveFileProcessor.Kendo.CodeFrame;

namespace FileProcessor.UnitTests.KendoParsingTests
{
    [TestFixture]
    public class ArgumentTests
    {
        [Test]
        public void Argument_Assigned_ToStringReturnsArgument()
        {
            var arg = new StringArgument("argTestName");

            Assert.AreEqual("argTestName", arg.ToString());
        }
    }
}
