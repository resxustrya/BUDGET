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
    [NoCache]
    [OutputCache(NoStore = true, Duration = 0)]
    public class AllotmentsController : Controller
    {
        BudgetDB db = new BudgetDB();
        // GET: Allotments
        public ActionResult Index()
        {
            var allotments = (from list in db.allotments where list.year == GlobalData.Year select list).ToList();
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
            allotments.Code = collection.Get("code");
            allotments.year = GlobalData.Year;
            db.allotments.Add(allotments);
            db.SaveChanges();

            ORSMaster orsmaster = new ORSMaster();
            orsmaster.Title = collection.Get("code");
            orsmaster.allotments = allotments.ID;
            orsmaster.Year = GlobalData.Year;
            db.orsmaster.Add(orsmaster);
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

            var orsmaster = db.orsmaster.Where(p => p.allotments == id).FirstOrDefault();
            orsmaster.Title = collection.Get("title");

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
                try
                {
                    var orsmaster = (from ors in db.orsmaster where ors.allotments == ID select ors).FirstOrDefault();
                    db.orsmaster.Remove(orsmaster);
                }
                catch { }
               

                var fsh = db.fsh.Where(p => p.allotment == ID.ToString()).ToList();
                foreach(FundSourceHdr f in fsh)
                {
                    var fsa = db.fsa.Where(p => p.fundsource == f.ID.ToString()).ToList();
                    db.fsa.RemoveRange(fsa);
                }
                db.fsh.RemoveRange(fsh);
                db.SaveChanges();
            }
            catch { }
            return RedirectToAction("Index");
        }
        public ActionResult FundSource(String ID)
        {
            int id = Convert.ToInt32(ID);
            var allotment = db.allotments.Where(p => p.ID == id).FirstOrDefault();
            GlobalData.allotment = allotment.ID.ToString();
            var fundsources = (from list in db.fsh where list.allotment == allotment.ID.ToString() select list).ToList();
            ViewBag.Message = @GlobalData.Year + " Budget Fund Source for " + allotment.Code;
            return View(fundsources);
        }
        public ActionResult CreateFundSource()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateFundSource(FormCollection collection)
        {
            FundSourceHdr fsh = new FundSourceHdr();
            fsh.prexc = collection.Get("prexcode");
            fsh.SourceTitle = collection.Get("source_title");
            fsh.desc = collection.Get("description");
            fsh.Code = collection.Get("title_code");
            fsh.allotment = GlobalData.allotment;
            db.fsh.Add(fsh);
            db.SaveChanges();
            String data = collection.Get("data");
            SaveFundSourceExpese(fsh.ID.ToString(), data);
            return PartialView("_Ok");
        }
        [HttpGet]
        public ActionResult EditFundSource(String id)
        {
            Int32 ID = Convert.ToInt32(id);
            var fsh = db.fsh.Where(p => p.ID == ID).FirstOrDefault();
            return View(fsh);
        }
        [HttpPost]
        public ActionResult SaveEditFundSource(FormCollection collection)
        {
            Int32 id = Convert.ToInt32(collection.Get("ID"));
            var fsh = db.fsh.Where(p => p.ID == id).FirstOrDefault();
            fsh.SourceTitle = collection.Get("source_title");
            fsh.prexc = collection.Get("prexcode");
            fsh.desc = collection.Get("description");
            fsh.Code = collection.Get("title_code");
            db.SaveChanges();
            String data = collection.Get("data");
            SaveFundSourceExpese(id.ToString(), data);
            return PartialView("_Ok");
        }
        public ActionResult DeleteFundSource(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var fsh = db.fsh.Where(p => p.ID == id).FirstOrDefault();
            db.fsh.Remove(fsh);
            var fsa = db.fsa.Where(p => p.fundsource == id.ToString()).ToList();
            db.fsa.RemoveRange(fsa);
            db.SaveChanges();
            return RedirectToAction("FundSource", new { id = GlobalData.allotment });
        }
        [Route("get/fundsource/expense",Name = "get_fund_source_expense")]
        public JsonResult GetFundSourceExpense(String fsh)
        {
            var fsa = (from list in db.fsa
                       join expensecode
                        in db.uacs on list.expensecode equals expensecode.Code
                       where list.fundsource == fsh
                       select new
                       {
                           ID = list.ID,
                           ExpenseCode = list.expensecode,
                           Title = expensecode.Title,
                           Amount = list.amount
                       }
                       );
            return Json(fsa, JsonRequestBehavior.AllowGet);
        }
        [Route("save/fundsource/expense",Name ="save_fundsource_expese")]
        public Boolean SaveFundSourceExpese(String fundsource, String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            Int32 id = 0;
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    id = Convert.ToInt32(sb.ID);
                    var fsa = db.fsa.Where(p => p.ID == id && p.fundsource == fundsource).FirstOrDefault();
                    fsa.expensecode = sb.expense_code;
                    fsa.amount = Convert.ToDouble(sb.amount);
                    try { db.SaveChanges(); } catch { }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if (sb.expense_code != null)
                        {
                            Object uacs_obj = sb.expense_code;
                            String uacs = uacs_obj.ToString();
                            var expense_exist = (from exist in db.fsa where exist.expensecode == uacs && exist.fundsource == fundsource select exist).ToList();
                            if(expense_exist.Count <= 0)
                            {
                                FundSourceAmount fsa = new FundSourceAmount();
                                fsa.expensecode = sb.expense_code;
                                fsa.amount = Convert.ToDouble(sb.amount);
                                fsa.fundsource = fundsource;
                                db.fsa.Add(fsa);
                                try { db.SaveChanges(); } catch { }
                            }
                        }

                    }
                    catch { }
                }

            }
            return true;
            
        }
        [Route("delete/fundsource/amount",Name ="delete_fund_source_amount")]
        public JsonResult DeleteFundSourceAmount(String data)
        {
            try
            {
                dynamic ors = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(ors.ID);

                var delete_fsa = db.fsa.Where(p => p.ID == ID).FirstOrDefault();
                db.fsa.Remove(delete_fsa);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return Json(true, JsonRequestBehavior.AllowGet);
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

        public ActionResult SubAllotment(String allotment)
        {
            Session.Add("allotment", allotment);
            var saahdr = db.saahdr.Where(p => p.allotment == allotment ).ToList();

            var details = (from alt in db.allotments
                           join fsh in db.fsh on alt.ID.ToString() equals fsh.allotment
                           select new
                           {
                               Allotment = alt.Code,
                              
                           }).FirstOrDefault();

            ViewBag.Header = "Sub-Allotment for " + details.Allotment;
            return View(saahdr);
        }
        public ActionResult CreateSubAllotment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSubAllotment(FormCollection collection)
        {
            SubAllotmentHeader saahdr = new SubAllotmentHeader();
            saahdr.prexc = collection.Get("prexcode");
            saahdr.allotment = Session["allotment"].ToString();
            saahdr.Title = collection.Get("source_title");
            saahdr.TitleCode = collection.Get("title_code");
            saahdr.allotment_for = collection.Get("description");
            db.saahdr.Add(saahdr);
            db.SaveChanges();

            String data = collection.Get("data");
            SaveSubAllotmentsAmount(saahdr.ID.ToString(), data);
            ViewBag.Message = "Sub-allotment entry successfully created";
            return PartialView("_PartialSubAllotment");
        }
        public ActionResult EditSubAllotment(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var saahdr = db.saahdr.Where(p => p.ID == id).FirstOrDefault();
            return View(saahdr);
        }

        [Route("get/saamt", Name = "get_saaamt")]
        public JsonResult GetSaaAmt(String saahdr)
        {
            var fsa = (from list in db.saaamount
                       join expensecode
                        in db.uacs on list.expensecode equals expensecode.Code
                       where list.saahdr == saahdr
                       select new
                       {
                           ID = list.ID,
                           ExpenseCode = list.expensecode,
                           Title = expensecode.Title,
                           Amount = list.amount
                       }
                       );
            return Json(fsa, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult EditSubAllotment(FormCollection collection)
        {
            Int32 id = Convert.ToInt32(collection.Get("ID"));
            var saahdr = db.saahdr.Where(p => p.ID == id).FirstOrDefault();
            saahdr.prexc = collection.Get("prexcode");
            saahdr.allotment = Session["allotment"].ToString();
            saahdr.Title = collection.Get("source_title");
            saahdr.TitleCode = collection.Get("title_code");
            saahdr.allotment_for = collection.Get("description");
            db.SaveChanges();

            String data = collection.Get("data");
            SaveSubAllotmentsAmount(saahdr.ID.ToString(), data);
            ViewBag.Message = "Sub-allotment entry successfully edited";
            return PartialView("_PartialSubAllotment");
        }
        
        public ActionResult DeleteSubAllotment(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var saahdr = db.saahdr.Where(p => p.ID == id).FirstOrDefault();
            db.saahdr.Remove(saahdr);

            var saaamt = db.saaamount.Where(p => p.saahdr == id.ToString()).ToList();
            db.saaamount.RemoveRange(saaamt);

            db.SaveChanges();

            return RedirectToAction("SubAllotment", "Allotments", new { allotment = Session["allotment"].ToString()});
        }
        public void SaveSubAllotmentsAmount(String saahdr, String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            Int32 id = 0;
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    id = Convert.ToInt32(sb.ID);
                    var saaamount = db.saaamount.Where(p => p.ID == id && p.saahdr == saahdr).FirstOrDefault();
                    saaamount.expensecode = sb.expense_code;
                    saaamount.amount = Convert.ToDouble(sb.amount);
                    try { db.SaveChanges(); } catch { }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if (sb.expense_code != null)
                        {
                            Object uacs_obj = sb.expense_code;
                            String uacs = uacs_obj.ToString();
                            var expense_exist = (from exist in db.fsa where exist.expensecode == uacs && exist.fundsource == saahdr select exist).ToList();
                            if (expense_exist.Count <= 0)
                            {
                                SubAllotmentAmounts saaamount = new SubAllotmentAmounts();
                                saaamount.expensecode = sb.expense_code;
                                saaamount.amount = Convert.ToDouble(sb.amount);
                                saaamount.saahdr = saahdr;
                                db.saaamount.Add(saaamount);
                                try { db.SaveChanges(); } catch { }
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        public ActionResult Realignment(String fundsource)
        {
            ViewBag.fundsource = fundsource;
            return View();
        }

    }
}