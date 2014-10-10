using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RecursiveFileProcessor.Kendo;
using RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo;
using RecursiveFileProcessor.Kendo.Parser;

namespace FileProcessor.UnitTests.KendoParsingTests
{
    [TestFixture]
    public class MigrationTests
    {

        [Test]
        public void DataKeysMovementTest()
        {
            var parser = new CodeParser();

            parser.Parse(@"Html.Kendo().Grid<Order>()
    .Name(""Grid"")       
    .DataKeys(dataKeys => dataKeys.Add(o => o.OrderID));
");

            var migration = new DataKeysMovement();
            var logger = new Logger();

            migration.ApplyTo(parser.Code.Statements[0], logger);

            Debug.Write(parser.Code);
        }

       
    }
}
