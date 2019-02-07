using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using BUDGET.Filters;
namespace BUDGET.Controllers
{
    [Authorize]
    [OutputCache(Duration = 0)]
    public class BudgetsController : Controller
    {
        public BudgetDB db = new BudgetDB();
        // GET: Budgets
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var yearlybudget = from yb in db.yearbudget where yb.active == 1 orderby yb.Year descending select yb;
            return View(yearlybudget.ToPagedList(pageIndex,pageSize));
        }
        public ActionResult Year()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Year(FormCollection collection)
        {
            try
            {
                Int32 year = Convert.ToInt32(collection.Get("year"));
                var yearBudget = db.yearbudget.Where(p => p.Year == year && p.active == 1).FirstOrDefault();
                if (yearBudget != null)
                {
                    GlobalData.Year = yearBudget.Year.ToString();
                    Session["year"] = yearBudget.ID;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch { }
            TempData["Error"] = "Input did match any records";
            return RedirectToAction("Year");
        }
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [CustomAuthorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Int32 year = Convert.ToInt32(collection.Get("year"));

                var yearBudget = db.yearbudget.Where(p => p.Year == year && p.active == 1).FirstOrDefault();
                if (yearBudget == null)
                {
                    YearBudget yb = new YearBudget();
                    yb.Year = year;
                    yb.CreatedBy = User.Identity.GetUserName();
                    yb.active = 1;
                    db.yearbudget.Add(yb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["Error"] = "Invalid Data";
                return RedirectToAction("Create");
            }
            
            TempData["Error"] = "Year referrence already exist";
            return RedirectToAction("Create");
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult DeleteYear(String id)
        {
            Int32 ID = Convert.ToInt32(id);
            var year = db.yearbudget.Where(p => p.ID == ID).FirstOrDefault();
            year.active = 0;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}