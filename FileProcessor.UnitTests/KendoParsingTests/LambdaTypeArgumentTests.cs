using NUnit.Framework;
using RecursiveFileProcessor.Kendo.CodeFrame;

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
    }
}
