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
    
    [CustomAuthorize(Roles ="Admin,Encoder")]
    [YearlyFilter]
    [NoCache]
    [OutputCache(NoStore = true, Duration = 0)]
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
            ViewBag.allotments = ors.allotments;
            return View();
        }
        [Route("get/ors/ps",Name = "get_ors_ps")]
        public JsonResult GetOrsPS()
        {
            Int32 ors_id = Convert.ToInt32(GlobalData.ors_id);
            var orsps = (from list in db.ors where list.ors_id == ors_id
                      orderby list.FundSource ascending
                      select new
                      {
                          ID = list.ID,
                          Row = list.Row,
                          Date = list.Date1,
                          DB = list.DB,
                          PO = list.PO,
                          PR = list.PR,
                          PAYEE = list.PAYEE,
                          Adress = list.Adress,
                          Particulars = list.Particulars,
                          FundSource = list.FundSource,
                          Gross = (from ors_uacs in db.ors_expense_codes where ors_uacs.ors_obligation == list.ID select ors_uacs.amount).DefaultIfEmpty(0).Sum(),
                          Created_By = list.Created_By,
                          DateReceived = list.DateReceived,
                          TimeReceived = list.TimeReceived,
                          DateReleased = list.DateReleased,
                          TimeReleased = list.TimeReleased,
                      }).ToList();
            return Json(orsps, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult GetHeadRequestingHeads()
        {
            var head_requesting_head = (from list in db.ors_head_request
                                        select new
                                        {
                                            head = list.Name
                                        }).ToList();
            return Json(head_requesting_head, JsonRequestBehavior.AllowGet);
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
                   
                    Object date = sb.Date;
                    ors.Date1 = date.ToString();
                    DateTime datetime = Convert.ToDateTime(date.ToString());
                    ors.Date = datetime;
                    ors.DB = sb.DB;
                    ors.PO = sb.PO;
                    ors.PR = sb.PR;
                    ors.PAYEE = sb.PAYEE;
                    ors.Adress = sb.Adress;
                    ors.Particulars = sb.Particulars;
                    ors.FundSource = sb.FundSource;
                    ors.DateReceived = sb.DateReceived;
                    ors.TimeReceived = sb.TimeReceived;
                    ors.DateReleased = sb.DateReleased;
                    ors.TimeReleased = sb.TimeReleased;
                    ors.head_requesting_office = sb.head_requesting;

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

                            Object date = sb.Date;
                            ors.Date1 = date.ToString();
                            DateTime datetime = Convert.ToDateTime(date.ToString());
                            ors.Date = datetime;

                            ors.DB = sb.DB;
                            ors.PO = sb.PO;
                            ors.PR = sb.PR;
                            ors.PAYEE = sb.PAYEE;
                            ors.Adress = sb.Adress;
                            ors.Particulars = sb.Particulars;
                            
                            ors.FundSource = sb.FundSource;
                            
                            ors.Created_By = User.Identity.GetUserName();
                            

                            ors.DateReceived = sb.DateReceived;
                            ors.TimeReceived = sb.TimeReceived;
                            ors.DateReleased = sb.DateReleased;
                            ors.TimeReleased = sb.TimeReleased;
                            ors.Date_Added = DateTime.Now.Date;
                            ors.dateadded = DateTime.Now.Date.ToString();
                            ors.head_requesting_office = sb.head_requesting;
                            db.ors.Add(ors);
                            try { db.SaveChanges(); } catch { }

                            Notifications notifications = new Notifications();

                            notifications.Message = "added a new ors obligation in";
                            notifications.User = User.Identity.GetUserName();
                            notifications.Module = "ORS";

                            var action = (from ors_list in db.ors
                                          join ors_master in db.orsmaster on ors_list.ors_id equals ors_master.ID
                                          select new
                                          {
                                              Title = ors_master.Title
                                          }).First();

                            notifications.Action = action.Title.ToString();

                            db.notifications.Add(notifications);
                            db.SaveChanges();
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
        
        public PartialViewResult  ORS_UACS(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var ors = db.ors.Where(p => p.ID == id).FirstOrDefault();
            ViewBag.ors_obligation = ID;
            return PartialView(ors);
        }

        public JsonResult GetORSUacs(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var ors_uacs = (from list in db.ors_expense_codes
                            join uacs in db.uacs on list.uacs equals uacs.Code
                            where list.ors_obligation == id
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

        public JsonResult GetFundSourceUACS(String ID)
        {
            Int32 id = Convert.ToInt32(ID);

            var fund_source_uacs = (from uacs in db.uacs
                                    select new
                                    {
                                        Code = uacs.Code
                                    }).ToList();

            return Json(fund_source_uacs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveOrsObligation(FormCollection collection)
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

                            if (uacs_exist.Count <= 0)
                            {

                                ORS_EXPENSE_CODES oec = new ORS_EXPENSE_CODES();
                                oec.uacs = sb.expense_code;
                                oec.ors_obligation = id;
                                oec.amount = sb.amount;
                                db.ors_expense_codes.Add(oec);
                                try { db.SaveChanges(); } catch { }


                                var ors_allotments = (from ors in db.ors
                                                      join ors_master in db.orsmaster on ors.ors_id equals ors_master.ID
                                                      join allotments in db.allotments on ors_master.allotments equals allotments.ID
                                                      where ors.ID == id
                                                      select new
                                                      {
                                                          _allotment = allotments.ID,
                                                          fundsource_id = (from _fsh in db.fsh where _fsh.allotment == allotments.ID.ToString() && _fsh.Code == ors.FundSource select _fsh.ID).FirstOrDefault()
                                                      }).FirstOrDefault();


                                var ors_fundsource_uacs = (from _fsa in db.fsa where _fsa.fundsource == ors_allotments.fundsource_id.ToString() && _fsa.expensecode == oec.uacs select _fsa.ID).ToList();
                                
                                if(ors_fundsource_uacs.Count <= 0)
                                {
                                    FundSourceAmount new_fsa = new FundSourceAmount();
                                    new_fsa.expensecode = oec.uacs;
                                    new_fsa.amount = 0;
                                    new_fsa.fundsource = ors_allotments.fundsource_id.ToString();
                                    db.fsa.Add(new_fsa);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            
            return GetORSUacs(id.ToString());
        }
        [HttpPost]
        public JsonResult DeleteUacs(FormCollection collection)
        {
            String ID = collection.Get("ID");
            Int32 id = Convert.ToInt32(ID);
            var remove_uacs = db.ors_expense_codes.Where(p => p.ID == id).FirstOrDefault();
            db.ors_expense_codes.Remove(remove_uacs);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFundSource(String ID)
        {
            var ors_allotments = db.fsh.Where(p => p.allotment == ID).ToList();

            return Json(ors_allotments, JsonRequestBehavior.AllowGet);
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