using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BUDGET.DataHelpers;
using BUDGET.Models;
namespace BUDGET.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        BudgetDB db = new BudgetDB();
        // GET: Reports
        public ActionResult DownloadSaob()
        {
            var allotments = (from list in db.allotments where list.year == GlobalData.Year select list).ToList();
            return PartialView(allotments);
        }
    }
}