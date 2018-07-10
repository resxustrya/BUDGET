﻿using System;
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
            model.notifications = db.notifications.ToList();
            model.orsmaster = db.orsmaster.ToList();
            return View(model);
        }
    }
}