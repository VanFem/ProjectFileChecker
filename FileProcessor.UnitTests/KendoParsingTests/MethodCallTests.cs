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
    public class MethodCallTests
    {
        [Test]
        public void MethodCall_EmptyMethodCall_ReturnsEmptyString()
        {
            var mc = new MethodCall();

            Assert.AreEqual(string.Empty, mc.ToString());
        }
    }
}
