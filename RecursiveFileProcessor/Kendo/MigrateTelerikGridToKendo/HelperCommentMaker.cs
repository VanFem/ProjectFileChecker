using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo
{
    public static class HelperCommentMaker
    {
        private const string ViewModelPostfix = "ViewModel";

        public static string ViewModelName { get; set; }
        public static string ReadOperation { get; set; }
        public static string CreateOperation { get; set; }
        public static string UpdateOperation { get; set; }
        public static string DestroyOperation { get; set; }
        public static string Controller { get; set; }
        public static string EntityName { get; set; }

        public static string GetHelperComment()
        {
            if (!string.IsNullOrEmpty(ViewModelName))
            {
                EntityName = ViewModelName.EndsWith(ViewModelPostfix)
                    ? ViewModelName.Substring(0, ViewModelName.Length - ViewModelPostfix.Length)
                    : string.Format("[{0}_KABAN]", ViewModelName);
            }

            if (string.IsNullOrEmpty(EntityName) || 
                string.IsNullOrEmpty(Controller) ||
                string.IsNullOrEmpty(ViewModelName))
                return string.Empty;

            // {0} - Read Operation
            // {1} - Create Operation
            // {2} - Update Operation
            // {3} - Destroy Operation
            // {4} - Controller
            // {5} - ViewModelName
            // {6} - EntityName
            return string.Format(@"<!-- @Html.ActionLink(""-"", ""{0}"", ""{4}"") -->
        
        @*
        
        #region Dependencies

        [Inject]
        public GridEntityCRUDHelper<{6}, {5}> {6}CRUDHelper {{ get; set; }}

        #endregion

        [HttpPost]
        public ActionResult {0}([DataSourceRequest]DataSourceRequest request, int id)
        {{
            return Json({6}CRUDHelper.Read(request, id));
        }}

        [HttpPost]
        public ActionResult {1}([DataSourceRequest]DataSourceRequest request, {5} model)
        {{
            return Json({6}CRUDHelper.Create(request, model));
        }}

        [HttpPost]
        public ActionResult {2}([DataSourceRequest]DataSourceRequest request, {5} model)
        {{
            return Json({6}CRUDHelper.Update(request, model));
        }}

        [HttpPost]
        public ActionResult {3}([DataSourceRequest]DataSourceRequest request, int id)
        {{
            return Json({6}CRUDHelper.Destroy(request, id));
        }}
        
        *@", ReadOperation, CreateOperation, UpdateOperation, DestroyOperation, Controller, ViewModelName, EntityName);
        }
    }
}
