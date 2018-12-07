using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.DataHelpers;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using BUDGET.Filters;
using PagedList;
using System.Threading.Tasks;

namespace BUDGET
{
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

        [CustomAuthorize(Roles = "Admin,Encoder,Cashier")]
        public ActionResult OrsItem(String ID)
        {
            String query = Request.QueryString["q"] ?? "";
            Session["query"] = query;
            var allotment = db.allotments.Where(p => p.ID.ToString() == ID).FirstOrDefault();
            GlobalData.ors_allotment = allotment.ID.ToString();
            ViewBag.Menu = allotment.year + " | " + allotment.Code;
            ViewBag.allotments = allotment.ID.ToString();
            return View();
        }

        [CustomAuthorize(Roles = "Admin,Encoder,Cashier")]

        [Route("get/ors/ps", Name = "get_ors_ps")]
        public JsonResult GetOrsPS()
        {
            String query = Session["query"].ToString().ToLower();
            Int32 ors_allotment = Convert.ToInt32(GlobalData.ors_allotment);
            var orsps = (from list in db.ors
                         where list.allotment == ors_allotment
                         orderby list.Date_Added ascending
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
                             Disbursement = (from ors_uacs in db.ors_expense_codes where ors_uacs.ors_obligation == list.ID select ors_uacs.TaxAmount + ors_uacs.NetAmount + ors_uacs.Others).DefaultIfEmpty(0).Sum(),
                             Created_By = list.Created_By,
                             DateReceived = list.DateReceived,
                             TimeReceived = list.TimeReceived,
                             DateReleased = list.DateReleased,
                             TimeReleased = list.TimeReleased,
                         }).ToList();

            
            if (query.Length > 0)
            {
                try
                {
                    var query_list = (from p in orsps where p.DB.ToLower().Contains(query) select p).ToList();
                    if (query_list.Count > 0)
                    {
                        return Json(query_list, JsonRequestBehavior.AllowGet);
                    }
                } catch { }

                try
                {
                    var query_list = (from p in orsps where p.PO.ToLower().Contains(query) select p).ToList();
                    if (query_list.Count > 0)
                    {
                        return Json(query_list, JsonRequestBehavior.AllowGet);
                    }
                }
                catch { }
                try
                {
                    var query_list = (from p in orsps where p.PR.ToLower().Contains(query) select p).ToList();
                    if (query_list.Count > 0)
                    {
                        return Json(query_list, JsonRequestBehavior.AllowGet);
                    }
                }
                catch { }

                try
                {
                    var query_list = (from p in orsps where p.Adress.ToLower().Contains(query) select p).ToList();
                    if (query_list.Count > 0)
                    {
                        return Json(query_list, JsonRequestBehavior.AllowGet);
                    }
                }
                catch { }

                try
                {
                    var query_list = (from p in orsps where p.PAYEE.ToLower().Contains(query) select p).ToList();
                    if (query_list.Count > 0)
                    {
                        return Json(query_list, JsonRequestBehavior.AllowGet);
                    }
                }
                catch { }
                try
                {
                    var query_list = (from p in orsps where p.Particulars.ToLower().Contains(query) select p).ToList();
                    if (query_list.Count > 0)
                    {
                        return Json(query_list, JsonRequestBehavior.AllowGet);
                    }
                }
                catch { }

                try
                {
                    var query_list = (from p in orsps where p.FundSource.ToLower().Contains(query) select p).ToList();
                    if (query_list.Count > 0)
                    {
                        return Json(query_list, JsonRequestBehavior.AllowGet);
                    }
                }
                catch { }
            }
            return Json(orsps, JsonRequestBehavior.AllowGet);
        }


        [Route("save/ors/ps",Name = "save_ors_ps")]
        [CustomAuthorize(Roles = "Admin,Encoder")]
        public JsonResult SaveORPS(String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            Int32 id = 0;
            Int32 ors_allotment = Convert.ToInt32(GlobalData.ors_allotment);
            Int32 rowCount = 0;
            String DateFormat = "yyyy-MM-dd HH:mm:ss";
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    String fundsource = (String)sb.FundSource;
                    var fundsource_exist = db.fsh.Where(p => p.allotment.ToString() == GlobalData.ors_allotment && p.Code == fundsource).ToList();

                    if(fundsource_exist.Count > 0)
                    {
                        id = Convert.ToInt32(sb.ID);
                        var ors = db.ors.Where(p => p.ID == id).Where(p => p.allotment == ors_allotment).FirstOrDefault();
                        Object date = sb.Date;
                        ors.Date1 = date.ToString();
                        DateTime datetime = Convert.ToDateTime(date.ToString());
                        ors.Row = sb.Row;
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
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        String fundsource = (String)sb.FundSource;
                        var fundsource_exist = db.fsh.Where(p => p.allotment.ToString() == GlobalData.ors_allotment && p.Code == fundsource).ToList();

                        if(fundsource_exist.Count > 0)
                        {
                            if (sb.Date != null && sb.Particulars != null && sb.PAYEE != null)
                            {
                                ORS ors = new ORS();
                                ors.allotment = Convert.ToInt32(GlobalData.ors_allotment);
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
                                ors.Date_Added = DateTime.Now;
                                ors.dateadded = DateTime.Now.ToString(DateFormat);
                                ors.head_requesting_office = sb.head_requesting;
                                db.ors.Add(ors);
                                try { db.SaveChanges(); } catch { }

                                try
                                {
                                    var ors_master = db.allotments.Where(p => p.ID.ToString() == GlobalData.ors_allotment).FirstOrDefault();
                                    Notifications notifications = new Notifications();
                                    notifications.Module = "ORS, " + ors_master.Title;
                                    notifications.User = User.Identity.GetUserName();
                                    notifications.Action = " added a new ors obligation in";
                                    notifications.DateAdded = DateTime.Now;
                                    db.notifications.Add(notifications);
                                    db.SaveChanges();
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                }
            }
            return GetOrsPS();
        }

        [CustomAuthorize(Roles = "Admin,Encoder")]
        [Route("delete/ors/ps",Name = "delete_ors_ps")]
        public ActionResult DeleteORSPS(String data)
        {
            try
            {
                dynamic ors = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(ors.ID);

                var del_ors = db.ors.Where(p => p.ID == ID).FirstOrDefault();

                var ors_uacs = db.ors_expense_codes.Where(p => p.ors_obligation == del_ors.ID).ToList();
                db.ors_expense_codes.RemoveRange(ors_uacs);
                
                db.ors.Remove(del_ors);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(Roles = "Admin,Encoder,Cashier")]
        public PartialViewResult  ORS_UACS(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var ors = db.ors.Where(p => p.ID == id).FirstOrDefault();
            ViewBag.ors_obligation = ID;
            return PartialView(ors);
        }

        [CustomAuthorize(Roles = "Admin,Encoder,Cashier")]
        public JsonResult GetORSUacs(String ID)
        {
            Int32 id = Convert.ToInt32(ID);
            var ors_uacs = (from list in db.ors_expense_codes
                            join uacs in db.uacs on list.uacs equals uacs.Title
                            where list.ors_obligation == id
                            select
                            new
                            {
                                ID = list.ID,
                                ExpenseTitle = list.uacs,
                                Amount = list.amount,
                                Disbursement = list.NetAmount + list.TaxAmount + list.Others,
                                ExpenseCode = uacs.Code,
                                NetAmount = list.NetAmount,
                                TaxAmount = list.TaxAmount,
                                Others = list.Others
                            }).ToList();
            return Json(ors_uacs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFundSourceUACS(String ID)
        {
            Int32 id = Convert.ToInt32(ID);

            var fund_source_uacs = (from uacs in db.uacs
                                    select new
                                    {
                                        Code = uacs.Title
                                    }).ToList();

            return Json(fund_source_uacs, JsonRequestBehavior.AllowGet);
        }


        [CustomAuthorize(Roles = "Admin,Encoder,Cashier")]
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
                    String expenese_title = (String)sb.expense_title;
                    var uacs = db.uacs.Where(p => p.Title == expenese_title).FirstOrDefault();
                    if(uacs != null)
                    {
                        line_id = Convert.ToInt32(sb.ID);
                        var ors_uacs = db.ors_expense_codes.Where(p => p.ID == line_id).FirstOrDefault();

                        if (User.IsInRole("Admin") || User.IsInRole("Encoder"))
                        {
                            ors_uacs.uacs = sb.expense_title;

                            try { ors_uacs.amount = Convert.ToDouble(sb.amount); } catch { ors_uacs.amount = 0.00; }
                            try { ors_uacs.NetAmount = Convert.ToDouble(sb.NetAmount); } catch { ors_uacs.NetAmount = 0.00; }
                            try { ors_uacs.TaxAmount = Convert.ToDouble(sb.TaxAmount); } catch { ors_uacs.TaxAmount = 0.00; }
                            try { ors_uacs.Others = Convert.ToDouble(sb.Others); } catch { ors_uacs.Others = 0.00; }
                        }
                        else if (User.IsInRole("Cashier"))
                        {
                            try { ors_uacs.NetAmount = Convert.ToDouble(sb.NetAmount); } catch { ors_uacs.NetAmount = 0.00; }
                            try { ors_uacs.TaxAmount = Convert.ToDouble(sb.TaxAmount); } catch { ors_uacs.TaxAmount = 0.00; }
                            try { ors_uacs.Others = Convert.ToDouble(sb.Others); } catch { ors_uacs.Others = 0.00; }
                        }

                        try { db.SaveChanges(); } catch { }
                    }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        String expenese_title = (String)sb.expense_title;
                        var uacs = db.uacs.Where(p => p.Title == expenese_title).FirstOrDefault();
                        if(uacs != null)
                        {
                            if (User.IsInRole("Admin") || User.IsInRole("Cashier"))
                            {
                                var uacs_exist = (from exist in db.ors_expense_codes where exist.uacs == expenese_title && exist.ors_obligation == id select exist).ToList();
                                if (uacs_exist.Count <= 0)
                                {

                                    ORS_EXPENSE_CODES oec = new ORS_EXPENSE_CODES();
                                    oec.uacs = sb.expense_title;
                                    oec.ors_obligation = id;
                                    try { oec.amount = Convert.ToDouble(sb.amount); } catch { oec.amount = 0.00; }
                                    try { oec.TaxAmount = Convert.ToDouble(sb.TaxAmount); } catch { oec.TaxAmount = 0.00; }
                                    try { oec.NetAmount = Convert.ToDouble(sb.NetAmount); } catch { oec.NetAmount = 0.00; }
                                    try { oec.Others = Convert.ToDouble(sb.Others); } catch { oec.Others = 0.00; }
                                    db.ors_expense_codes.Add(oec);

                                    db.SaveChanges();

                                    var ors_allotments = (from ors in db.ors
                                                          join allotments in db.allotments on ors.allotment equals allotments.ID
                                                          where ors.ID == id
                                                          select new
                                                          {
                                                              _allotment = allotments.ID,
                                                              fundsource_id = (from _fsh in db.fsh where _fsh.allotment == allotments.ID.ToString() && _fsh.Code == ors.FundSource select _fsh.ID).FirstOrDefault()
                                                          }).FirstOrDefault();

                                    var ors_fundsource_uacs = (from _fsa in db.fsa where _fsa.fundsource == ors_allotments.fundsource_id.ToString() && _fsa.expense_title == oec.uacs select _fsa.ID).ToList();

                                    if (ors_fundsource_uacs.Count <= 0)
                                    {
                                        FundSourceAmount new_fsa = new FundSourceAmount();
                                        new_fsa.expense_title = oec.uacs;
                                        new_fsa.amount = 0;
                                        new_fsa.fundsource = ors_allotments.fundsource_id.ToString();
                                        db.fsa.Add(new_fsa);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception innerex) { }
                }
            }
            return GetORSUacs(id.ToString());
        }
        [HttpPost]
        [CustomAuthorize(Roles = "Admin,Encoder")]
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
            return PartialView();
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
        [HttpGet]
        public ActionResult UpdateOrsHeadRequest(String ID)
        {
            var ors_head = db.ors_head_request.Where(p => p.ID.ToString() == ID).FirstOrDefault();
            Session["ORS_HEAD"] = ID;
            return PartialView(ors_head);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateOrsHeadRequest(FormCollection collection)
        {
            String ID = Session["ORS_HEAD"].ToString();
            var ors_head = db.ors_head_request.Where(p => p.ID.ToString() == ID).FirstOrDefault();
            ors_head.Name = collection.Get("name");
            ors_head.Position = collection.Get("position");
            db.SaveChanges();
            return RedirectToAction("ors_head_request_office");
        }
        public ActionResult DeleteORSHeadRequest(String ID)
        {
            var ors_head = db.ors_head_request.Where(p => p.ID.ToString() == ID).FirstOrDefault();
            db.ors_head_request.Remove(ors_head);
            db.SaveChanges();
            return RedirectToAction("ors_head_request_office");
        }
        [HttpGet]
        public ActionResult GetOrsDateView(String uacs, Int32 ors_id,String code)
        {
            ViewBag.uacs = uacs;
            ViewBag.ors_id = ors_id;
            ViewBag.code = code;
            return PartialView();
        }
        [HttpGet]
        public JsonResult GetOrsDateJson(Int32 ors_id, String expense_code, String expense_title)
        {
            var ors_date_entry = (from list in db.ors_date_entry
                                  where list.ExpenseTitle == expense_title && list.ors_id == ors_id && list.ExpenseCode == expense_code
                                  select new
                                  {
                                      ID = list.ID,
                                      Date = list.StringDate,
                                      Amount = list.amount,
                                      TaxAmount = list.TaxAmount,
                                      NetAmount = list.NetAmount,
                                      Others = list.Others,
                                      Disbursement = list.TaxAmount + list.NetAmount + list.Others
                                  }).ToList();
            return Json(ors_date_entry, JsonRequestBehavior.AllowGet);
        }
        public void SaveOrsDAteJson(FormCollection collection)
        {
            Int32 ors_id = Convert.ToInt32(collection.Get("ors_id"));
            String expense_title = collection.Get("expense_title");
            String expense_code = collection.Get("expense_code");
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(collection.Get("data"));

            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    Int32 id = Convert.ToInt32(sb.ID);
                    var ors_date = db.ors_date_entry.Where(p => p.ID == id).FirstOrDefault();
                    ors_date.Date = Convert.ToDateTime(sb.Date);
                    ors_date.StringDate = sb.Date;
                    try { ors_date.amount = Convert.ToDouble(sb.Amount); } catch { ors_date.amount = 0.00; }
                    try { ors_date.NetAmount = Convert.ToDouble(sb.NetAmount); } catch { ors_date.NetAmount = 0.00; }
                    try { ors_date.TaxAmount = Convert.ToDouble(sb.TaxAmount); } catch { ors_date.TaxAmount = 0.00; }
                    try { ors_date.Others = Convert.ToDouble(sb.Others); } catch { ors_date.Others = 0.00; }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        OrsDateEntry ors_date = new OrsDateEntry();
                        ors_date.ors_id = ors_id;
                        ors_date.ExpenseCode = expense_code;
                        ors_date.ExpenseTitle = expense_title;
                        ors_date.Date = Convert.ToDateTime(sb.Date);
                        ors_date.StringDate = sb.Date;
                        try { ors_date.amount = Convert.ToDouble(sb.Amount); } catch { ors_date.amount = 0.00; }
                        try { ors_date.NetAmount = Convert.ToDouble(sb.NetAmount); } catch { ors_date.NetAmount = 0.00; }
                        try { ors_date.TaxAmount = Convert.ToDouble(sb.TaxAmount); } catch { ors_date.TaxAmount = 0.00; }
                        try { ors_date.Others = Convert.ToDouble(sb.Others); } catch { ors_date.Others = 0.00; }
                        db.ors_date_entry.Add(ors_date);
                    }
                    catch (Exception innerex) { }
                }
            }
            db.SaveChanges();
        }
    }
}