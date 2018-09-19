using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.Models;
using Newtonsoft;
using Newtonsoft.Json;
using BUDGET.Filters;
namespace BUDGET.Controllers
{
    [Authorize]
    [YearlyFilter]
    [NoCache]
    [OutputCache(NoStore = true, Duration = 0)]
    public class ExpenseCodesController : Controller
    {
        // GET: ExpenseCodes
        BudgetDB db = new BudgetDB();

        public ActionResult ExpenseCodes()
        {
            ViewBag.Menu = "Expense Codes (UACS)";
            return View();
        }
        [Route("get/expense/codes",Name = "get_expense_codes")]
        public JsonResult GetExpenseCodes()
        {
            var uacs = (from list in db.uacs
                      orderby list.Line ascending
                      select new
                      {
                          ID = list.ID,
                          Line = list.Line,
                          Title = list.Title,
                          Code = list.Code,
                      }).ToList();
            return Json(uacs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("save/expense/codes",Name = "save_expense_codes")]
        public JsonResult SaveExpenseCode(String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            Int32 id = 0;
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    //var ps = db.ps.Where(p => p.ID == sb.ID).FirstOrDefault();
                    id = Convert.ToInt32(sb.ID);
                    var uacs = db.uacs.Where(p => p.ID == id).FirstOrDefault();
                    uacs.Line = sb.Line;
                    uacs.Title = sb.Title;
                    uacs.Code = sb.Code;
                    try { db.SaveChanges(); } catch { }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if(sb.Title != null && sb.Code != null)
                        {
                            UACS uacs = new UACS();
                            uacs.Line = sb.Line;
                            uacs.Code = sb.Code;
                            uacs.Title = sb.Title;
                            db.uacs.Add(uacs);
                            try { db.SaveChanges(); } catch { }
                        }
                    }
                    catch { }
                }
            }
            return GetExpenseCodes();
        }
        [Authorize(Roles = "Admin")]
        [Route("delete/expense/codes",Name = "delete_expense_codes")]
        public String DeleteExpenseCode()
        {
            return "";
        }
        
        [Route("get/expense/codes/title",Name = "get_expense_codes_title")]
        public JsonResult GetExpenseCodeNumber()
        {
            var uacs = (from list in db.uacs
                        orderby list.Line ascending
                        select new
                        {
                            Expense = list.Title
                        }).ToList();
            return Json(uacs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Prexc()
        {
            ViewBag.Menu = "FUND SOURCE | PREXC";
            return View();
        }
        [Route("get/prexc",Name ="get_prexc")]
        public JsonResult GetPrexc()
        {
            var prexc = (from list in db.prexc
                         orderby list.Line ascending
                         select new
                         {
                             ID = list.ID,
                             Line = list.Line,
                             Desc = list.Desc,
                             Code1 = list.Code1,
                             Code2 = list.Code2
                         }).ToList();
            return Json(prexc, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin")]
        [Route("save/prexc",Name ="save_prexc")]
        public JsonResult SavePrexc(String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            Int32 id = 0;
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    //var ps = db.ps.Where(p => p.ID == sb.ID).FirstOrDefault();
                    id = Convert.ToInt32(sb.ID);
                    var prexc = db.prexc.Where(p => p.ID == id).FirstOrDefault();
                    prexc.Line = sb.Line;
                    prexc.Desc = sb.Desc;
                    prexc.Code1 = sb.Code1;
                    prexc.Code2 = sb.Code2;
                    try { db.SaveChanges(); } catch { }
                }
               
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if (sb.Desc != null && sb.Code1 != null && sb.Code2 != null)
                        {
                            PREXC prexc = new PREXC();
                            prexc.Line = sb.Line;
                            prexc.Desc = sb.Desc;
                            prexc.Code1 = sb.Code1;
                            prexc.Code2 = sb.Code2;
                            db.prexc.Add(prexc);
                            try { db.SaveChanges(); } catch { }
                        }
                    }
                    catch { }
                }
            }
            return GetPrexc();
        }
        [Authorize(Roles = "Admin")]
        [Route("delete/prexc",Name ="delete_prexc")]
        public String DeletePrexc(String data)
        {
            return "";
        }
        [Route("get/prexc/number",Name = "get_prexc_number")]
        public JsonResult GetPrexcCodeNumber()
        {
            var prexc_list = (from list in db.prexc
                              select new
                              {
                                  prexc = list.Code1
                              }).ToList();
            return Json(prexc_list, JsonRequestBehavior.AllowGet);
        }
    }
}