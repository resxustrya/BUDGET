using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.Filters;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using BUDGET.Models;
namespace BUDGET.Controllers
{
    [Authorize]
    [YearlyFilter]
    [NoCache]
    [OutputCache(NoStore = true, Duration = 0)]
    public class FundSourceController : Controller
    {
        BudgetDB db = new BudgetDB();
        // GET: FundSource
        public ActionResult Index()
        {
            ViewBag.Menu = "Fund Source";
            return View();
        }
        
    }
}