using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace BUDGET
{
    [AllowAnonymous]
    public class BudgetSummaryController : Controller
    {
        private BudgetDB db = new BudgetDB();
        // GET: BudgetSummary
        [Route("summary",Name ="summary")]
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetSaob(String datefrom = "", String dateto= "")
        {
            ViewBag.datefrom = datefrom;
            ViewBag.dateto = dateto;
            return PartialView(db);
        }
    }
}