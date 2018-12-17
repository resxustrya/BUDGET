using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using System.Web.Security;
using BUDGET.Models;

namespace BUDGET.Controllers
{
    [Authorize(Roles ="Admin")]
    [YearlyFilter]
    public class UserAccountController : Controller
    {
        BudgetDB db = new BudgetDB();
        ApplicationDbContext context = new ApplicationDbContext();
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> UserManager;
        // GET: UserAccount
        public UserAccountController()
        {
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }
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
        [HttpGet]
        public ActionResult Update(String userId)
        {
            
            ViewBag.Roles = context.Roles.ToList();
            var user = UserManager.FindById(userId);
            Session["edituser_userid"] = user.Id;
            ViewBag.UserRole = context.Roles.Find(user.Roles.SingleOrDefault().RoleId).Name;
            return PartialView(user);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(FormCollection collection)
        {
            
            String userId = Session["edituser_userid"].ToString();
            var user = UserManager.FindById(userId);
            var old_user_role = context.Roles.Find(user.Roles.SingleOrDefault().RoleId).Name;

            if(collection.Get("role") != old_user_role)
            {
                UserManager.RemoveFromRole(userId, old_user_role);
                UserManager.AddToRole(userId, collection.Get("role"));
            }
            
            user.UserName = collection.Get("username");
            UserManager.Update(user);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ResetPassword(String userId)
        {
            var user = UserManager.FindById(userId);
            Session["reset_userid"] = user.Id;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(FormCollection collection)
        {
            var user = UserManager.FindById(Session["reset_userid"].ToString());
            UserManager.RemovePassword(user.Id);
            var store = new UserStore<ApplicationUser>(context);
            var new_password = UserManager.PasswordHasher.HashPassword(collection.Get("password"));
            store.SetPasswordHashAsync(user, new_password);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(String userId)
        {
            //var user = UserManager.FindById(userId);
            var user = context.Users.Where(p => p.Id == userId).FirstOrDefault();
            if(user.UserName != "doh7budget")
            {
                context.Users.Remove(user);
                context.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }

    }
}