using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.Models;
using BUDGET.Filters;
using Newtonsoft.Json;
using System.Threading;
using Microsoft.AspNet.Identity;
namespace BUDGET
{
    [Authorize(Roles = "Admin")]
    [YearlyFilter]
    [NoCache]
    [OutputCache(NoStore = true, Duration = 0)]
    public class AllotmentsController : Controller
    {
        BudgetDB db = new BudgetDB();
        // GET: Allotmentss
        public ActionResult Index()
        {
            var allotments = (from list in db.allotments where list.active == 1 && list.year == GlobalData.Year orderby list.Code2 ascending select list).ToList();
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
            allotments.active = 1;
            allotments.year = GlobalData.Year;
            db.allotments.Add(allotments);

            Notifications n = new Notifications();

            n.Action = "added " + allotments.Code + " budget allotment to";
            n.Module = "Budget Allocations";
            n.User = User.Identity.GetUserName();
            n.Year = GlobalData.Year;
            n.DateAdded = DateTime.Now;
            n.status = "add";
            db.notifications.Add(n);

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
            allotments.Code = collection.Get("code");

            db.SaveChanges();
            return RedirectToAction("Index");
        }
            
        public ActionResult Delete(String id)
        {
            try
            {
                int ID = Convert.ToInt32(id);
                var del_allot = db.allotments.Where(p => p.ID == ID).FirstOrDefault();
                del_allot.active = 0;

                db.Database.ExecuteSqlCommand("UPDATE FundSourceHdrs SET active = 0 WHERE allotment ='" + ID.ToString() + "'");

                Notifications n = new Notifications();

                n.Action = "removed " + del_allot.Code + " allocation from";
                n.Module = "Budget Allocations";
                n.User = User.Identity.GetUserName();
                n.Year = GlobalData.Year;
                n.DateAdded = DateTime.Now;
                n.status = "delete";
                db.notifications.Add(n);

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
            var fundsources = (from list in db.fsh where list.allotment == allotment.ID.ToString() && list.type == "REG" && list.active == 1 select list).ToList();
            ViewBag.Message = @GlobalData.Year + " Budget Fund Source for " + allotment.Code;
            ORS ors = new ORS();
            
            return View(fundsources);
        }
        
        public ActionResult CreateFundSource()
        {
            return View();
        }
        [HttpPost]
        public String CreateFundSource(FormCollection collection)
        {
            
            FundSourceHdr fsh = new FundSourceHdr();
            fsh.prexc = collection.Get("prexcode");
            fsh.SourceTitle = collection.Get("source_title").TrimEnd();
            fsh.desc = collection.Get("description").TrimEnd();
            fsh.Code = collection.Get("title_code").TrimEnd();
            fsh.Responsibility_Number = collection.Get("responsibility_number").TrimEnd();
            fsh.type = "REG";
            fsh.allotment = GlobalData.allotment;
            fsh.active = 1;
            db.fsh.Add(fsh);
            db.SaveChanges();
            String data = collection.Get("data");
            SaveFundSourceExpese(fsh.ID.ToString(), data);
            Session["saved"] = "saved";
            return Url.Action("EditFundSource", "Allotments", new { id = fsh.ID });
        }
        [HttpGet]
        public ActionResult EditFundSource(String id)
        {
            Int32 ID = Convert.ToInt32(id);
            var fsh = db.fsh.Where(p => p.ID == ID).FirstOrDefault();
            return View(fsh);
        }

        [HttpPost]
        public String SaveEditFundSource(FormCollection collection)
        {
            Int32 id = Convert.ToInt32(collection.Get("ID"));
            var fsh = db.fsh.Where(p => p.ID == id).FirstOrDefault();
            fsh.SourceTitle = collection.Get("source_title").TrimEnd();
            fsh.prexc = collection.Get("prexcode").TrimEnd();
            fsh.desc = collection.Get("description").TrimEnd();
            fsh.Code = collection.Get("title_code").TrimEnd();
            fsh.Responsibility_Number = collection.Get("responsibility_number").TrimEnd();
            
            db.SaveChanges();
            String data = collection.Get("data");
            SaveFundSourceExpese(id.ToString(), data);
            Session["saved"] = "saved";
            return Url.Action("EditFundSource", "Allotments", new { id = fsh.ID });
        }

        public ActionResult DeleteFundSource(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var fsh = db.fsh.Where(p => p.ID == id).FirstOrDefault();
            fsh.active = 0;
            db.SaveChanges();
            return RedirectToAction("FundSource", new { id = GlobalData.allotment });
        }

        [Route("get/fundsource/expense",Name = "get_fund_source_expense")]
        public JsonResult GetFundSourceExpense(String fsh)
        {
            var fsa = (from list in db.fsa
                       group list by list.ID into g
                       join expensecode
                        in db.uacs on g.FirstOrDefault().expense_title equals expensecode.Title
                       where g.FirstOrDefault().fundsource == fsh
                       select new
                       {
                           ID = g.FirstOrDefault().ID,
                           ExpenseCode = g.FirstOrDefault().expense_title,
                           Amount = g.FirstOrDefault().amount
                       });
           
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
                    if (UacsHelper.ExpenseExist(Convert.ToString(sb.expense_title)))
                    {
                        id = Convert.ToInt32(sb.ID);
                        var fsa = db.fsa.Where(p => p.ID == id && p.fundsource == fundsource).FirstOrDefault();
                        fsa.expense_title = sb.expense_title;
                        fsa.amount = Convert.ToDouble(sb.amount);
                        try { db.SaveChanges(); } catch { }
                    }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if (UacsHelper.ExpenseExist(Convert.ToString(sb.expense_title)))
                        {
                            if (sb.expense_title != null)
                            {
                                String uacs = (String)sb.expense_title;
                                var expense_exist = (from exist in db.fsa where exist.expense_title == uacs && exist.fundsource == fundsource select exist).ToList();
                                if (expense_exist.Count <= 0)
                                {
                                    FundSourceAmount fsa = new FundSourceAmount();
                                    fsa.expense_title = uacs;
                                    try { fsa.amount = Convert.ToDouble(sb.amount); } catch { fsa.amount = 0.00; }
                                    fsa.fundsource = fundsource;
                                    db.fsa.Add(fsa);
                                    try { db.SaveChanges(); } catch { }
                                }
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
        
        public ActionResult CreateOrs()
        {
            return View();
        }
        

        public ActionResult SubAllotment(String allotment)
        {
            Session.Add("allotment", allotment);
            var saahdr = db.fsh.Where(p => p.allotment == allotment && p.type == "SUB" && p.active == 1).ToList();
            var details = (from alt in db.allotments
                           join fsh in db.fsh on alt.ID.ToString() equals fsh.allotment
                           select new
                           {
                               Allotment = alt.Code,
                           }).FirstOrDefault();
            
            ViewBag.Header = "Sub-Allotment for " + details.Allotment;
            return View(saahdr);
        }
        [HttpGet]
        public ActionResult CreateSubAllotment()
        {
            return View();
        }

        [HttpPost]
        public String CreateSubAllotment(FormCollection collection)
        {
            FundSourceHdr fsh = new FundSourceHdr();
            fsh.prexc = collection.Get("prexcode").TrimEnd();
            fsh.allotment = Session["allotment"].ToString();
            fsh.SourceTitle = collection.Get("source_title").TrimEnd();
            fsh.Code = collection.Get("title_code").TrimEnd();
            fsh.desc = collection.Get("description").TrimEnd();
            fsh.Responsibility_Number = collection.Get("responsibility_number").TrimEnd();
            fsh.type = "SUB";
            fsh.active = 1;
            db.fsh.Add(fsh);
            db.SaveChanges();
            String data = collection.Get("data");
            SaveSubAllotmentsAmount(fsh.ID.ToString(), data);
            Session["saved"] = "saved";
            return Url.Action("EditSubAllotment", "Allotments", new { id = fsh.ID });
        }

        public ActionResult EditSubAllotment(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var fsh = db.fsh.Where(p => p.ID == id && p.type == "SUB" && p.active == 1).FirstOrDefault();
            return View(fsh);
        }


        [Route("get/saamt", Name = "get_saaamt")]
        public JsonResult GetSaaAmt(String fsh)
        {
            var fsa = (from list in db.fsa
                       join expensecode
                        in db.uacs on list.expense_title equals expensecode.Title
                       where list.fundsource == fsh
                       select new
                       {
                           ID = list.ID,
                           Expense = list.expense_title,
                           Amount = list.amount
                       }).ToList();
            return Json(fsa, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public String EditSubAllotment(FormCollection collection)
        {
            Int32 id = Convert.ToInt32(collection.Get("ID"));
            var fsh = db.fsh.Where(p => p.ID == id && p.type == "SUB").FirstOrDefault();
            fsh.prexc = collection.Get("prexcode");
            fsh.allotment = Session["allotment"].ToString();
            fsh.SourceTitle = collection.Get("source_title");
            fsh.Code = collection.Get("title_code");
            fsh.desc = collection.Get("description");
            fsh.Responsibility_Number = collection.Get("responsibility_number");
            db.SaveChanges();

            String data = collection.Get("data");
            SaveFundSourceExpese(fsh.ID.ToString(), data);
            return Url.Action("EditSubAllotment", "Allotments", new { id = fsh.ID });
        }

        public void SaveSubAllotmentsAmount(String fsh, String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            Int32 id = 0;
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    if(UacsHelper.ExpenseExist(Convert.ToString(sb.expense_title)))
                    {
                        id = Convert.ToInt32(sb.ID);
                        var fsa = db.fsa.Where(p => p.ID == id && p.fundsource == fsh).FirstOrDefault();
                        fsa.expense_title = sb.expense_title;
                        fsa.amount = Convert.ToDouble(sb.amount);
                        try { db.SaveChanges(); } catch { }
                    }
                    
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if(UacsHelper.ExpenseExist(Convert.ToString(sb.expense_title)))
                        {
                            if (sb.expense_title != null)
                            {
                                Object uacs_obj = sb.expense_title;
                                String uacs = uacs_obj.ToString();
                                var expense_exist = (from exist in db.fsa where exist.expense_title == uacs && exist.fundsource == fsh select exist).ToList();
                                if (expense_exist.Count <= 0)
                                {
                                    FundSourceAmount fsa = new FundSourceAmount();
                                    fsa.expense_title = sb.expense_title;
                                    try { fsa.amount = Convert.ToDouble(sb.amount); } catch { fsa.amount = 0.00; }
                                    fsa.fundsource = fsh;
                                    db.fsa.Add(fsa);
                                    try { db.SaveChanges(); } catch { }
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        public JsonResult DeleteSaaAmt(String data)
        {
            try
            {
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(obj.ID);
                var saa_amt = db.fsa.Where(p => p.ID == ID).FirstOrDefault();
                db.fsa.Remove(saa_amt);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteSubAllotment(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var fsh = db.fsh.Where(p => p.ID == id && p.type == "SUB").FirstOrDefault();
            fsh.active = 0;
            db.SaveChanges();
            return RedirectToAction("SubAllotment", "Allotments", new { allotment = Session["allotment"].ToString()});
        }

        public ActionResult Realignment(String fundsource)
        {
            var allotment = (from _fsh in db.fsh join _allotment in db.allotments on _fsh.allotment
                             equals _allotment.ID.ToString()
                             where _fsh.ID.ToString() == fundsource
                             select new
                             {
                                 Allotment = _allotment.Code,
                                 FundSource = _fsh.Code
                             }).FirstOrDefault();

            ViewBag.Head = allotment.Allotment + " " + allotment.FundSource;
            ViewBag.fundsource = fundsource;
            return View();
        }
        public JsonResult GetFundSourceUacs(String fundsource)
        {
            var _fsa = (from list in db.fsa where list.fundsource == fundsource select new
            {
                uacs = list.expense_title
            }).ToList();
            return Json(_fsa, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRealignment(String fundsource, String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            Int32 id = 0;
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    id = Convert.ToInt32(sb.ID);
                    if(UacsHelper.ExpenseExist(Convert.ToString(sb.uacs_from)) && UacsHelper.ExpenseExist(Convert.ToString(sb.uacs_to)))
                    {
                        var realignment = db.realignment.Where(p => p.ID == id && p.fundsource == fundsource).FirstOrDefault();
                        realignment.uacs_from = sb.uacs_from;
                        realignment.uacs_to = sb.uacs_to;
                        realignment.amount = Convert.ToDouble(sb.amount);
                        try { db.SaveChanges(); } catch { }
                    }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if(UacsHelper.ExpenseExist(Convert.ToString(sb.uacs_from)) && UacsHelper.ExpenseExist(Convert.ToString(sb.uacs_to)))
                        {
                            if (sb.uacs_from != null && sb.uacs_to != null)
                            {
                                Object uacs_from_obj = sb.uacs_from;
                                Object uacs_to_ojb = sb.uacs_to;

                                String uacs_from = uacs_from_obj.ToString();
                                String uacs_to = uacs_to_ojb.ToString();

                                var realignment_exist = (from exist in db.realignment where exist.uacs_from == uacs_from && exist.uacs_to == uacs_to select exist).ToList();
                                if (realignment_exist.Count <= 0)
                                {
                                    Realignment realignment = new Realignment();
                                    realignment.uacs_from = uacs_from;
                                    realignment.uacs_to = uacs_to;
                                    realignment.amount = Convert.ToDouble(sb.amount);
                                    realignment.fundsource = fundsource;
                                    db.realignment.Add(realignment);
                                    try { db.SaveChanges(); } catch { }
                                }
                            }
                        }
                    }
                    catch { }
                }
            }

            var realignment_fundsource = (from _fsh in db.fsh
                                          join _allotment in db.allotments on _fsh.allotment equals _allotment.ID.ToString()
                                          where _fsh.ID.ToString() == fundsource
                                          select new
                                          {
                                              FundSource = _fsh.Code,
                                              Allotment = _allotment.Code
                                          }).FirstOrDefault();

            Notifications notifications = new Notifications()
            {
                DateAdded = DateTime.Now,
                Message = "Added a new realignment in",
                Module = "Realignment",
                Action = realignment_fundsource.Allotment
            };
            return GetRealignments(fundsource);
        }
        public JsonResult GetRealignments(String fundSource)
        {
            var realignments = (from _realignment in db.realignment
                                join _fsa in db.fsa on _realignment.fundsource equals _fsa.fundsource
                                where _realignment.fundsource == fundSource && _realignment.uacs_from == _fsa.expense_title
                                select new
                                {
                                    ID = _realignment.ID,
                                    uacs_from = _realignment.uacs_from,
                                    uacs_to = _realignment.uacs_to,
                                    realignment_amt = _realignment.amount
                                }).ToList();

            return Json(realignments, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteRealignment(String data)
        {
            try
            {
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(obj.ID);

                var _realignment = db.realignment.Where(p => p.ID == ID).FirstOrDefault();
                db.realignment.Remove(_realignment);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public JsonResult ORS_HEAD(FormCollection collection)
        {
            try
            {
                Int32 ID = Convert.ToInt32(collection.Get("fsh"));
                var fsh = db.fsh.Where(p => p.ID == ID).FirstOrDefault();
                fsh.ors_head = Convert.ToInt32(collection.Get("ors_head"));
                db.SaveChanges();
            }
            catch { }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExpenseSuballotment(String fundsource, String uacs)
        {
            
            var fsh = db.fsh.Where(p => p.ID.ToString() == fundsource).FirstOrDefault();
            var expensecode = db.uacs.Where(p => p.Title == uacs).FirstOrDefault();

            ViewBag.Title = fsh.Code;
            ViewBag.UACS_CODE = expensecode.Title;
            ViewBag.fundsource = fundsource;
            ViewBag.uacs = uacs;
            return PartialView();
        }

        public ActionResult GetExpenseCodeAllotment(String fundsource ,String ExpenseTitle)
        {
            //var uacs_sub = (from list in db.uacs_sub_codes where)
            return Json(true,JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveExpenseSubAllotment(FormCollection collection)
        {

           
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(collection.Get("data"));
            
            Int32 id = 0;
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    id = Convert.ToInt32(sb.ID);
                   // var fsa = db.expense_suballotment_expensecode.Where(p => p.ID == id ).FirstOrDefault();
                   //fsa.expensecode = sb.expense_code;
                   // fsa.amount = Convert.ToDouble(sb.amount);
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
                            var expense_exist = (from exist in db.fsa where exist.expense_title == uacs && exist.fundsource == collection.Get("fundsource") select exist).ToList();
                            if (expense_exist.Count <= 0)
                            {
                                FundSourceAmount fsa = new FundSourceAmount();
                                fsa.expense_title = sb.expense_title;
                                try { fsa.amount = Convert.ToDouble(sb.amount); } catch { fsa.amount = 0.00; }
                                fsa.fundsource = collection.Get("fundsource");
                                db.fsa.Add(fsa);
                                try { db.SaveChanges(); } catch { }
                            }
                        }
                    }
                    catch { }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AllotmentSeries(String allotment)
        {
            Int32 ID = Convert.ToInt32(allotment);
            var all = db.allotments.Where(p => p.ID == ID).FirstOrDefault();
            Session["edit_number"] = allotment;
            return PartialView(all);
        }
        [HttpPost]
        public ActionResult AllotmentSeries(FormCollection collection)
        {
            Int32 ID = Convert.ToInt32(Session["edit_number"].ToString());
            var allotment = db.allotments.Where(p => p.ID ==ID).FirstOrDefault();
            allotment.Code2 = collection.Get("number");
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public JsonResult Previous(FormCollection collection)
        {
            Int32 ID = Convert.ToInt32(collection.Get("allotment"));
            var allotment = db.allotments.Where(p => p.ID == ID).FirstOrDefault();
            if (collection.Get("checked") == "true")
            {
                allotment.previous = true;
            }
            else
            {
                allotment.previous = false;
            }

            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}