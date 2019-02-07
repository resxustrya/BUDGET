using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BUDGET
{
    [Authorize]
    [YearlyFilter]
    [NoCache]
    [OutputCache(NoStore = true, Duration = 0)]
    public class HomeController : Controller
    {
        BudgetDB db = new BudgetDB();
        // GET: Home
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            DateTime this_month = Convert.ToDateTime(DateTime.Now.ToString("MM") + "/1/" + DateTime.Now.ToString("yyyy"));
            model.notifications = (from n in db.notifications where n.DateAdded >= this_month && n.DateAdded <= DateTime.Now && n.Year == GlobalData.Year orderby n.DateAdded descending select n).ToList();
            model.allotments = db.allotments.Where(p => p.year == GlobalData.Year && p.active == 1).OrderBy(p => p.Code2).ToList();
            return View(model);
        }
    }
}