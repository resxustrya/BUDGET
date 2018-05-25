using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.Models;
using BUDGET.DataHelpers;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using BUDGET.Filters;
using PagedList;
namespace BUDGET.Controllers
{
    [Authorize]
    [YearlyFilter]
    [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
    public class ORSController : Controller
    {
        JsonGetter jg = new JsonGetter();
        BudgetDB db = new BudgetDB();
        // GET: ORS
        public ActionResult Index()
        {
            return View();
        }
        [Route("ors/ps", Name = "ors_ps")]
        public ActionResult ORPS()
        {
            ViewBag.Menu = GlobalData.Year + " ORS Personnel Services";
            return View();
        }
        public ActionResult OrsItem(String id)
        {
            Int32 ID = Convert.ToInt32(id);
            var ors = (from list in db.orsmaster where list.ID == ID && list.Year == GlobalData.Year select list).FirstOrDefault();
            GlobalData.ors_id = ors.ID.ToString();
            ViewBag.Menu = ors.Year + " | " + ors.Title;
            return View();
        }
        [Route("get/ors/ps",Name = "get_ors_ps")]
        public JsonResult GetOrsPS()
        {
            Int32 ors_id = Convert.ToInt32(GlobalData.ors_id);
            var orsps = (from list in db.ors where list.ors_id == ors_id
                      orderby list.Row ascending
                      select new
                      {
                          ID = list.ID,
                          Row = list.Row,
                          Date = list.Date,
                          DB = list.DB,
                          PO = list.PO,
                          PR = list.PR,
                          PAYEE = list.PAYEE,
                          Adress = list.Adress,
                          Particulars = list.Particulars,
                          ORS_NO = list.ORS_NO,
                          FundSource = list.FundSource,
                          Gross = (from ors_uacs in db.ors_expense_codes where ors_uacs.ors_obligation == list.ID select ors_uacs.amount).DefaultIfEmpty(0).Sum(),
                          AE = list.AE,
                          AF = list.AF,
                          AG = list.AG,
                          AH = list.AH,
                          AI = list.AI,
                          Created_By = list.Created_By,
                          DateReceived = list.DateReceived,
                          TimeReceived = list.TimeReceived,
                          DateReleased = list.DateReleased,
                          TimeReleased = list.TimeReleased
                      }).ToList();
            return Json(orsps, JsonRequestBehavior.AllowGet);
        }

        public Double GetGross(Int32 id)
        {
            Double total = 0.00;
            total = (from ors_uacs in db.ors_expense_codes where ors_uacs.ors_obligation == id select ors_uacs.amount).Sum();
            return total;
        }
        [Route("save/ors/ps",Name = "save_ors_ps")]
        public JsonResult SaveORPS(String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            Int32 id = 0;
            Int32 ors_id = Convert.ToInt32(GlobalData.ors_id);
            foreach (Object s in list)
            {   
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    
                    id = Convert.ToInt32(sb.ID);
                    var ors = db.ors.Where(p => p.ID == id).Where(p => p.ors_id == ors_id ).FirstOrDefault();
                    ors.Row = sb.Row;
                    ors.Date = sb.Date;
                    ors.DB = sb.DB;
                    ors.PO = sb.PO;
                    ors.PR = sb.PR;
                    ors.PAYEE = sb.PAYEE;
                    ors.Adress = sb.Adress;
                    ors.Particulars = sb.Particulars;
                    ors.ORS_NO = sb.ORS_NO;
                    ors.FundSource = sb.FundSource;
                    ors.AE = sb.AE;
                    ors.AF = sb.AF;
                    ors.AG = sb.AG;
                    ors.AH = sb.AH;
                    ors.AI = sb.AI;
                    ors.DateReceived = sb.DateReceived;
                    ors.TimeReceived = sb.TimeReceived;
                    ors.DateReleased = sb.DateReleased;
                    ors.TimeReleased = sb.TimeReleased;
               

                    try { db.SaveChanges(); } catch { }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if(sb.Date != null && sb.Particulars != null && sb.PAYEE != null)
                        {
                            ORS ors = new ORS();
                            ors.ors_id = Convert.ToInt32(GlobalData.ors_id);
                            ors.Row = sb.Row;
                            ors.Date = sb.Date;
                            ors.DB = sb.DB;
                            ors.PO = sb.PO;
                            ors.PR = sb.PR;
                            ors.PAYEE = sb.PAYEE;
                            ors.Adress = sb.Adress;
                            ors.Particulars = sb.Particulars;
                            ors.ORS_NO = sb.ORS_NO;
                            ors.FundSource = sb.FundSource;
                            ors.AE = sb.AE;
                            ors.AF = sb.AF;
                            ors.AG = sb.AG;
                            ors.AH = sb.AH;
                            ors.AI = sb.AI;
                            ors.Created_By = User.Identity.GetUserName();
                            ors.DateReceived = sb.DateReceived;
                            ors.TimeReceived = sb.TimeReceived;
                            ors.DateReleased = sb.DateReleased;
                            ors.TimeReleased = sb.TimeReleased;

                            db.ors.Add(ors);
                            try { db.SaveChanges(); } catch { }
                        }
                        
                    }
                    catch { }
                }

            }
            return GetOrsPS();
        }

        [Route("delete/ors/ps",Name = "delete_ors_ps")]
        public ActionResult DeleteORSPS(String data)
        {
            try
            {
                dynamic ors = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(ors.ID);

                var del_ors = db.ors.Where(p => p.ID == ID).FirstOrDefault();
                db.ors.Remove(del_ors);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult  ORS_UACS(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var ors = db.ors.Where(p => p.ID == id).FirstOrDefault();
            ViewBag.ors_obligation = ID;
            return View(ors);
        }
        public JsonResult GetORSUacs(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var ors_uacs = (from list in db.ors_expense_codes join uacs in db.uacs on list.uacs equals uacs.Code where list.ors_obligation == id
                            select
                            new
                            {
                                ID = list.ID,
                                ExpenseCode = list.uacs,
                                Title = uacs.Title,
                                Amount = list.amount
                            }).ToList();
            return Json(ors_uacs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveOrsObligation(FormCollection collection)
        {
            Int32 id = Convert.ToInt32(collection.Get("ID"));
            String data = collection.Get("data");
            Int32 line_id = 0;
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    line_id = Convert.ToInt32(sb.ID);
                    var ors_uacs = db.ors_expense_codes.Where(p => p.ID == line_id).FirstOrDefault();
                    ors_uacs.uacs = sb.expense_code;
                    ors_uacs.amount = sb.amount;
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

                            var uacs_exist = (from exist in db.ors_expense_codes where exist.uacs == uacs && exist.ors_obligation == id select exist).ToList();
                            if(uacs_exist.Count <= 0)
                            {
                                ORS_EXPENSE_CODES oec = new ORS_EXPENSE_CODES();
                                oec.uacs = sb.expense_code;
                                oec.ors_obligation = id;
                                oec.amount = sb.amount;
                                db.ors_expense_codes.Add(oec);
                                try { db.SaveChanges(); } catch { }
                            }
                        }
                    }
                    catch { }
                }
            }
            ViewBag.ors_obligation = id.ToString();
            return PartialView();
        }
        public ActionResult ors_head_request_office(int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var ors_request = (from list in db.ors_head_request select list).ToList();
            return View(ors_request.ToPagedList(pageIndex, pageSize));
        }
        [HttpGet]
        public ActionResult CreateOrsHeadRequest()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrsHeadRequest(FormCollection collection)
        {
            ors_head_request ohr = new ors_head_request();
            ohr.Name = collection.Get("name");
            ohr.Position = collection.Get("position");
            db.ors_head_request.Add(ohr);
            db.SaveChanges();
            return RedirectToAction("ors_head_request_office");
        }
    }
}