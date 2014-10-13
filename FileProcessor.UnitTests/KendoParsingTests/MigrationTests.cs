using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RecursiveFileProcessor.Kendo;
using RecursiveFileProcessor.Kendo.CodeFrame;
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
            (parser.Code.Statements[0]["DataSource"].Arguments[0] as LambdaTypeArgument).LambdaBody.Statements[0].MethodCalls.Add(new MethodCall("Ajax"));
            Debug.Write(parser.Code.ToString());
        }

        [Test]
        public void DataBindingMovementTest()
        {
            var parser = new CodeParser();

            parser.Parse(@"Html.Telerik().Grid<Order>()
    .Name(""Grid"")      
    .DataBinding(dataBinding => dataBinding.Ajax().Select(""_AjaxBinding"", ""Grid""));
");

            var migration = new DataBindingMovement();
            var logger = new Logger();

            migration.ApplyTo(parser.Code.Statements[0], logger);
            Debug.Write(parser.Code.ToString());
        }

        [Test]
        public void TestTestTest()
        {
            var parser = new CodeParser();

            parser.Parse(@"@(Html.Kendo().Grid(Model.CurriculumMatrixViewModelList)
          .Name(""Curriculum"")
          .DataKeys(keys => keys.Add(pf => pf.Id))
          .DataBinding(db => db.Ajax()
                               .OperationMode(GridOperationMode.Client)
                                   .Select(""Curriculum"", ""ChangeRequestFlexibleDelivery"")
                                   .Insert(""CurriculumInlineInsert"", ""ChangeRequestFlexibleDlivery"")
                                   .Update(""CurriculumInlineUpdate"", ""ChangeRequestFlexibleDelivery"")
                                   .Delete(""CurriculumInlineRemove"", ""ChangeRequestFlexibleDelivery"")
          )
              .ClientEvents(ce => ce.OnSave(""OnGrideSave"").OnError(""GridOnError""))
          .ToolBar(cmd => cmd.Insert().ButtonType(GridButtonType.ImageAndText))
          .Columns(col =>
          {
              col.Command(cmd =>
              {
                  cmd.Edit().ButtonType(GridButtonType.Text);
                  cmd.Delete().ButtonType(GridButtonType.Text);
              }).Width(40).Title(""Action"");
              col.Bound(pf => pf.Id).Visible(false);
              col.Bound(pf => pf.ProposalId).Visible(false);
              col.Bound(pf => pf.TitleofCourse).Title(""Title of the Course Include Course Prefix and Number"").Width(300);
              col.Bound(pf => pf.DeliveredNewFormat).Title(""Delivered in New Format"").Width(100);
              col.Bound(pf => pf.DeliveredTraditionalFormat).Title(""Delivered in Traditional Format"").Width(100);
              col.Bound(pf => pf.CourseCurrentlyTaught).Title(""Is this Course currently Taught as a Part of the Approved Program?"").Width(300).ClientTemplate(""<#= (CourseCurrentlyTaught==true) ? 'Yes' : 'No' #>"");
          })
          .Editable(editing => editing
                                   .Mode(GridEditMode.InForm)
                                   .InsertRowPosition(GridInsertRowPosition.Top)
                                   .FormHtmlAttributes(new { enctype = ""multipart/form-data"" })
                                   .DefaultDataItem(new CurriculumMatrixCRViewModel { ProposalId = Model.ProposalId })
          )
          .Pageable(p => p.PageSize(5))
          );
");
            Debug.WriteLine("=====BEFORE=====\r\n"+parser.Code);
            var migration = new DataBindingMovement();
            var migration2 = new DataKeysMovement();
            var migration3 = new OperationModeRemake();
            var migration4 = new CRUDRename();
            var migration5 = new ClientEventsRemake();
            var migration6 = new PagerSettingsMovement();
            var migration7 = new CommandRenamingAndButtonTypeRemoval();
            
            var logger = new Logger();

            migration4.ApplyTo(parser.Code.Statements[0], logger);
            migration.ApplyTo(parser.Code.Statements[0], logger);
            migration2.ApplyTo(parser.Code.Statements[0], logger);
            migration3.ApplyTo(parser.Code.Statements[0], logger);
            migration5.ApplyTo(parser.Code.Statements[0], logger);
            migration6.ApplyTo(parser.Code.Statements[0], logger);
            migration7.ApplyTo(parser.Code.Statements[0], logger);

            parser.Code.PostfixComment = "kaban";


            Debug.WriteLine("=====AFTER=====\r\n");
            Debug.Write(parser.Code.ToString());
        }


       
    }
}
