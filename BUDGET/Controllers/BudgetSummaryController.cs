﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BUDGET
{
    public class BudgetSummaryController : Controller
    {
        private BudgetDB db = new BudgetDB();
        // GET: BudgetSummary
        public ActionResult Index()
        {
            return View();
        }
    }
}