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
        public ActionResult Index(int? page)
        {
            Session.Remove("year");
            GlobalData.Year = null;
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var yearlybudget = from yb in db.yearbudget orderby yb.Year descending select yb;
            return View(yearlybudget.ToPagedList(pageIndex,pageSize));
        }
        public ActionResult Year(String id)
        {
            Int32 ID = Convert.ToInt32(id);
            var year = db.yearbudget.Where(p => p.ID == ID).FirstOrDefault();
            GlobalData.Year = year.Year.ToString();

            if(year != null)
            {
                Session["year"] = year.ID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
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