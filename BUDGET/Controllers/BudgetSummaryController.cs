using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}