using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Owin;
using BUDGET.Models;

namespace BUDGET.Controllers
{
    [Authorize(Roles ="Admin")]
    [YearlyFilter]
    public class UserAccountController : Controller
    {
        BudgetDB db = new BudgetDB();
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: UserAccount
        public ActionResult Index()
        {
            
            var userlogins = context.Users.ToList();

            return View(userlogins);
        }
        public ActionResult Add()
        {
            var roles = context.Roles.ToList();
            return PartialView(roles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection collection)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            String username = collection.Get("username");
            String password = collection.Get("password");
            String role = collection.Get("role");

            var user = new ApplicationUser();
            user.UserName = username;
            user.Email = "doh7budget@gmail.com";

            string userPWD = password;

            var chkUser = UserManager.Create(user, userPWD);

            //Add default User to Role Admin   
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, role);
                
            }

            return RedirectToAction("Index");
        }
    }
}