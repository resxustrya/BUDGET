using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.Models;
using BUDGET.Filters;
using Newtonsoft.Json;
namespace BUDGET.Controllers
{
    [Authorize]
    [YearlyFilter]
    public class AllotmentsController : Controller
    {
        BudgetDB db = new BudgetDB();
        // GET: Allotments
        public ActionResult Index()
        {
            var allotments = (from list in db.allotments select list).ToList();
            return View(allotments);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Allotments allotments = new Allotments();
            allotments.Title = collection.Get("Title");
            allotments.year = GlobalData.Year;
            db.allotments.Add(allotments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditBudget(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var allotments = db.allotments.Where(p => p.ID == id).FirstOrDefault();
            return View(allotments);
        }
        [HttpPost]
        public ActionResult EditBudget(FormCollection collection)
        {
            Int32 id = Convert.ToInt32(collection.Get("ID"));
            var allotments = db.allotments.Where(p => p.ID == id).FirstOrDefault();
            allotments.Title = collection.Get("title");
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(String id)
        {
            try
            {
                int ID = Convert.ToInt32(id);
                var del_allot = db.allotments.Where(p => p.ID == ID).FirstOrDefault();
                db.allotments.Remove(del_allot);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult FundSource(String ID)
        {
            int id = Convert.ToInt32(ID);
            var allotment = db.allotments.Where(p => p.ID == id).FirstOrDefault();
            GlobalData.BudgetID = allotment.ID.ToString();
            ViewBag.Menu = allotment.Title + " | Fund Source : ID " + allotment.ID;
            return View();
        }
        [Route("get/budget/source-fund",Name = "get_budget_source_fund")]
        public JsonResult GetBudgetFundSource()
        {
            int allotment = Convert.ToInt32(GlobalData.BudgetID);
            var data = (from list in db.budgetsourcefund join prexc in db.prexc on list.prexc equals prexc.Code1
                        where list.allotment == allotment
                        orderby list.Line ascending
                        select new
                        {
                            Line = list.Line,
                            PREXC = list.prexc,
                            Title = prexc.Desc
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [Route("save/budget/source-fund",Name = "save_budget_source_fund")]
        public JsonResult SaveBudgetSourceFund(String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            String prexc = "";
            Int32 allotment = 0;
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    prexc = sb.prexc;
                    allotment = Convert.ToInt32(GlobalData.BudgetID);
                    var budgetsourcefund = db.budgetsourcefund.Where(x => x.prexc == prexc && x.allotment == allotment).ToList();
                    if(budgetsourcefund.Count <= 0)
                    {
                        BudgetSourceFund new_budgetsourcefund = new BudgetSourceFund();
                        new_budgetsourcefund.Line = sb.Line;
                        new_budgetsourcefund.prexc = sb.prexc;
                        new_budgetsourcefund.allotment = Convert.ToInt32(GlobalData.BudgetID);
                        db.budgetsourcefund.Add(new_budgetsourcefund);
                        try { db.SaveChanges(); } catch { }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return GetBudgetFundSource();
        }

        public ActionResult View(String ID)
        {
            return View();
        }

        public ActionResult ORS()
        {
            var ors = (from list in db.orsmaster where list.Year == GlobalData.Year select list).ToList();
            return View(ors);
        }
        public ActionResult CreateOrs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateOrs(FormCollection collection)
        {
            ORSMaster orsmaster = new ORSMaster();
            orsmaster.Title = collection.Get("title");
            orsmaster.Year = GlobalData.Year;
            db.orsmaster.Add(orsmaster);
            db.SaveChanges();
            return RedirectToAction("ORS");
        }
        public ActionResult DeleteOrs(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var orsmaster = db.orsmaster.Where(p => p.ID == id).FirstOrDefault();
            db.orsmaster.Remove(orsmaster);
            db.SaveChanges();
            return RedirectToAction("ORS");
        }
        
        public ActionResult EditOrs(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var ors = db.orsmaster.Where(p => p.ID == id).FirstOrDefault();
            return View(ors);
        }
        [HttpPost]
        public ActionResult EditOrs(FormCollection collection)
        {
            Int32 ID = Convert.ToInt32(collection.Get("ID"));
            var ors = db.orsmaster.Where(p => p.ID == ID).FirstOrDefault();
            ors.Title = collection.Get("Title");
            db.SaveChanges();
            return RedirectToAction("ORS");
        }
    }
}