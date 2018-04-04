using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.DataHelpers;
using BUDGET.Models;
namespace BUDGET.Controllers
{
    public class JsonController : Controller
    {
        // GET: Json
        BudgetDB db = new BudgetDB();
        public String GetExpenseCodes()
        {
            return "hahahehe";
        }
    }
}