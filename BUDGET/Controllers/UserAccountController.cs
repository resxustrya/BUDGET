using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

namespace BUDGET.Controllers
{
    public class UserAccountController : Controller
    {
        BudgetDB db = new BudgetDB();
        // GET: UserAccount
        public ActionResult Index()
        {
            var users = db.users.ToList();
            return View(users);
        }
    }
}