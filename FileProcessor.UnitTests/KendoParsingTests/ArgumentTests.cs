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
