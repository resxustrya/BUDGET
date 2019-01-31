using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BUDGET.DataHelpers;
using BUDGET.Models;
using BUDGET.Filters;
namespace BUDGET.Controllers
{

    [CustomAuthorize(Roles = "Admin,Encoder")]
    [NoCache]
    [OutputCache(NoStore = true, Duration = 0)]
    public class ReportsController : Controller
    {
        BudgetDB db = new BudgetDB();
        
        // GET: Reports
        public ActionResult DownloadSaob()
        {
            var allotments = (from list in db.allotments where list.year == GlobalData.Year select list).ToList();
            return PartialView(allotments);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DownloadSaob(FormCollection collection)
        {
            rpt_saob rpt = new rpt_saob();
            String date_from = collection.Get("date_from");
            String date_to = collection.Get("date_to");
            FileStreamResult fsResult = null;
            
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            SaobExcel saobexcel = new SaobExcel();
            saobexcel.CreateExcel(date_from,date_to);
            var filesStream = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB_NEW2.xlsx"), FileMode.Open);
            fsResult = new FileStreamResult(filesStream, contentType);
            return fsResult;
        }

        public ActionResult DownloadSaobSheet2()
        {
            return PartialView();
        }
       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult DownloadSaobSheet2(FormCollection collection)
       {
            rpt_saobsheet2 rppt2 = new rpt_saobsheet2();
            String date_from = collection.Get("date_from");
            String date_to = collection.Get("date_to");
            rppt2.generate_saob(date_from, date_to);
            var fileStream = new FileStream(Server.MapPath("~/rpt_saob/saobsheet2.pdf"),
                                     FileMode.Open,
                                     FileAccess.Read
                                   );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;
       }
    }
}