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
    [Authorize]
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
            rpt.generate_saob();
            var fileStream = new FileStream(Server.MapPath("~/rpt_saob/saob.pdf"),
                                     FileMode.Open,
                                     FileAccess.Read
                                   );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;
        }
    }
}