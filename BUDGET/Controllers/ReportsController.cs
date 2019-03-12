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
    [OutputCache(NoStore = true, Duration = 0,Location = System.Web.UI.OutputCacheLocation.None)]
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
        public ActionResult OrsSummary(String allotment)
        {
            Int32 ID = Convert.ToInt32(allotment);
            var fundsource = db.fsh.Where(p => p.allotment == ID.ToString() && p.active == 1).OrderBy(p => p.Code).ToList();
            return PartialView(fundsource);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrsSummary(FormCollection collection, String[] fundsource)
        {
            OrsReportSummary rpt = new OrsReportSummary();
            DateTime dateFrom = Convert.ToDateTime(collection.Get("dateFrom"));
            DateTime dateTo = Convert.ToDateTime(collection.Get("dateTo"));
            Int32 allotmentID = Convert.ToInt32(Session["allotmentID"].ToString());
            rpt.CreateExcel(allotmentID,fundsource ,dateFrom, dateTo);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var filesStream = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/ORSSUMMARY2.xlsx"), FileMode.Open);
            FileStreamResult fsResult = new FileStreamResult(filesStream, contentType);
            return fsResult;
        }
    }
}