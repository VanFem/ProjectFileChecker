using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RecursiveFileProcessor.Kendo.CodeFrame;
using RecursiveFileProcessor.Kendo.Parser;

namespace FileProcessor.UnitTests.KendoParsingTests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void Parser_EmptyInput_ReturnsEmptyMethodBody()
        {
            var parser = new CodeParser();

            parser.Parse("");

            Assert.AreEqual(0, parser.Code.Statements.Count);
        }

        [Test]
        public void Parser_OnlyObjectNameInCode_Parses()
        {
            var parser = new CodeParser();

            parser.Parse("AnObject;");

            Assert.AreEqual(1, parser.Code.Statements.Count);
            Assert.IsInstanceOf(typeof (Statement), parser.Code.Statements[0]);
            Assert.AreEqual("AnObject", parser.Code.Statements[0].Obj);
        }

        [Test]
        public void Parser_ObjectWithANoArgumentMethodInCode_Parses()
        {
            var parser = new CodeParser();

            parser.Parse("AnObject.Method();");

            Assert.AreEqual(1, parser.Code.Statements.Count);

            Assert.IsInstanceOf(typeof (Statement), parser.Code.Statements[0]);
            Assert.AreEqual("AnObject", parser.Code.Statements[0].Obj);

            Assert.AreEqual(1, parser.Code.Statements[0].MethodCalls.Count);
            Assert.AreEqual("Method", parser.Code.Statements[0].MethodCalls[0].MethodName);

            Assert.AreEqual(0, parser.Code.Statements[0].MethodCalls[0].Arguments.Count);
        }

        [Test]
        public void Parser_ObjectWithArgumentedMethodInCode_Parses()
        {
            var parser = new CodeParser();

            parser.Parse("AnObject.Method(argument1, argument2);");

            Assert.AreEqual(1, parser.Code.Statements.Count);

            Assert.IsInstanceOf(typeof (Statement), parser.Code.Statements[0]);
            Assert.AreEqual("AnObject", parser.Code.Statements[0].Obj);

            Assert.AreEqual(1, parser.Code.Statements[0].MethodCalls.Count);
            Assert.AreEqual("Method", parser.Code.Statements[0].MethodCalls[0].MethodName);

            Assert.AreEqual(2, parser.Code.Statements[0].MethodCalls[0].Arguments.Count);
            Assert.IsInstanceOf(typeof (StringArgument),
                parser.Code.Statements[0].MethodCalls[0].Arguments[0]);
            Assert.IsInstanceOf(typeof (StringArgument),
                parser.Code.Statements[0].MethodCalls[0].Arguments[1]);
            Assert.AreEqual("argument1", parser.Code.Statements[0].MethodCalls[0].Arguments[0].ToString());
            Assert.AreEqual("argument2", parser.Code.Statements[0].MethodCalls[0].Arguments[1].ToString());
        }

        [Test]
        public void Parser_MultipleMethodCallsIncode_Parses()
        {
            var parser = new CodeParser();

            parser.Parse("AnObject.Method1(argument1).Method2(argument1,argument2);");

            Assert.AreEqual(1, parser.Code.Statements.Count);
            Assert.IsInstanceOf(typeof (Statement), parser.Code.Statements[0]);
            Assert.AreEqual("AnObject", parser.Code.Statements[0].Obj);

            Assert.AreEqual(2, parser.Code.Statements[0].MethodCalls.Count);
            Assert.AreEqual("Method1", parser.Code.Statements[0].MethodCalls[0].MethodName);
            Assert.AreEqual("Method2", parser.Code.Statements[0].MethodCalls[1].MethodName);

            Assert.AreEqual(1, parser.Code.Statements[0].MethodCalls[0].Arguments.Count);
            Assert.IsInstanceOf(typeof (StringArgument),
                parser.Code.Statements[0].MethodCalls[0].Arguments[0]);
            Assert.AreEqual("argument1", parser.Code.Statements[0].MethodCalls[0].Arguments[0].ToString());

            Assert.AreEqual(2, parser.Code.Statements[0].MethodCalls[1].Arguments.Count);
            Assert.IsInstanceOf(typeof (StringArgument),
                parser.Code.Statements[0].MethodCalls[1].Arguments[0]);
            Assert.IsInstanceOf(typeof (StringArgument),
                parser.Code.Statements[0].MethodCalls[1].Arguments[1]);
            Assert.AreEqual("argument1", parser.Code.Statements[0].MethodCalls[1].Arguments[0].ToString());
            Assert.AreEqual("argument2", parser.Code.Statements[0].MethodCalls[1].Arguments[1].ToString());
        }

        [Test]
        public void Parser_LambdaMethodArgument_Parses()
        {
            var parser = new CodeParser();

            parser.Parse("AnObject.Method(arg => Object.Method());");

            Assert.AreEqual(1, parser.Code.Statements.Count);
            Assert.IsInstanceOf(typeof (Statement), parser.Code.Statements[0]);
            Assert.AreEqual("AnObject", parser.Code.Statements[0].Obj);

            Assert.AreEqual(1, parser.Code.Statements[0].MethodCalls.Count);
            Assert.AreEqual("Method", parser.Code.Statements[0].MethodCalls[0].MethodName);

            Assert.AreEqual(1, parser.Code.Statements[0].MethodCalls[0].Arguments.Count);
            Assert.IsInstanceOf(typeof (LambdaTypeArgument),
                parser.Code.Statements[0].MethodCalls[0].Arguments[0]);
            Assert.AreEqual("arg => Object.Method()",
                parser.Code.Statements[0].MethodCalls[0].Arguments[0].ToString());
        }

        [Test]
        public void TestTestTest()
        {
            var parser = new CodeParser();
            //      parser.Parse("Object.Hello(a  , (a,b)  => abc);");
            //    parser.Parse("Object.Hello(a,(a,b)=>abc);");
            // parser.Parse("Object.Hello(a,(a,b)=>{abc;});");
            //parser.Parse("Object.Hello(a,(a,b)=>{abc; abd;});");
            parser.Parse(@"db.Programs.Where(
                p => programIdList.Contains(p.ProposalId))
                .OrderBy(p => p.Title)
                .Select(p => p.Proposal)
                .Cast<Proposal>()
                .ToList();");
            Debug.WriteLine(parser.Code["db"]["Where"]);
        }
    }
}
