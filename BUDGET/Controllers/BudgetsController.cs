using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.Models;
using Microsoft.AspNet.Identity;
using PagedList;
namespace BUDGET.Controllers
{
    [Authorize]
    public class BudgetsController : Controller
    {
        public BudgetDB db = new BudgetDB();
        // GET: Budgets
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var yearlybudget = from yb in db.yearbudget orderby yb.Year ascending select yb;
            return View(yearlybudget.ToPagedList(pageIndex,pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            YearBudget yb = new YearBudget();
            yb.Year = Convert.ToInt32(collection["year"].ToString());
            yb.CreatedBy = User.Identity.GetUserName();
            db.yearbudget.Add(yb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}