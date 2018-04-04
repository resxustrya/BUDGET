using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.Models;
using BUDGET.DataHelpers;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
namespace BUDGET.Controllers
{
    [Authorize]
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
            ViewBag.Menu = "ORS Personnel Services";
            return View();
        }
        [Route("get/ors/ps",Name = "get_ors_ps")]
        public JsonResult GetOrsPS()
        {
            var orsps = (from list in db.orsps
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
                          Gross = list.Gross,
                          EXP_CODE_1 = list.EXP_CODE_1,
                          Amount_1 = list.Amount_1,
                          EXP_CODE_2 = list.EXP_CODE_2,
                          Amount_2 = list.Amount_2,
                          EXP_CODE_3 = list.EXP_CODE_3,
                          Amount_3 = list.Amount_3,
                          EXP_CODE_4 = list.EXP_CODE_4,
                          Amount_4 = list.Amount_4,
                          EXP_CODE_5 = list.EXP_CODE_5,
                          Amount_5 = list.Amount_5,
                          EXP_CODE_6 = list.EXP_CODE_6,
                          Amount_6 = list.Amount_5,
                          EXP_CODE_7 = list.EXP_CODE_7,
                          Amount_7 = list.Amount_7,
                          EXP_CODE_8 = list.EXP_CODE_8,
                          Amount_8 = list.Amount_8,
                          EXP_CODE_9 = list.EXP_CODE_9,
                          Amount_9 = list.Amount_9,
                          AE = list.AE,
                          AF = list.AF,
                          AG = list.AG,
                          AH = list.AH,
                          AI = list.AI,
                          Created_By = list.Created_By
                      }).ToList();
            return Json(orsps, JsonRequestBehavior.AllowGet);
        }
        [Route("save/ors/ps",Name = "save_ors_ps")]
        public JsonResult SaveORPS(String data)
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
                    var orps = db.orsps.Where(p => p.ID == id).FirstOrDefault();
                    orps.Row = sb.Row;
                    orps.Date = sb.Date;
                    orps.DB = sb.DB;
                    orps.PO = sb.PO;
                    orps.PR = sb.PR;
                    orps.PAYEE = sb.PAYEE;
                    orps.Adress = sb.Adress;
                    orps.Particulars = sb.Particulars;
                    orps.ORS_NO = sb.ORS_NO;
                    orps.FundSource = sb.FundSource;
                    orps.Gross = sb.Gross;
                    orps.EXP_CODE_1 = sb.EXP_CODE_1;
                    orps.Amount_1 = sb.Amount_1;
                    orps.EXP_CODE_2 = sb.EXP_CODE_2;
                    orps.Amount_2 = sb.Amount_2;
                    orps.EXP_CODE_3 = sb.EXP_CODE_3;
                    orps.Amount_3 = sb.Amount_3;
                    orps.EXP_CODE_4 = sb.EXP_CODE_4;
                    orps.Amount_4 = sb.Amount_4;
                    orps.EXP_CODE_5 = sb.EXP_CODE_5;
                    orps.Amount_5 = sb.Amount_5;
                    orps.EXP_CODE_6 = sb.EXP_CODE_6;
                    orps.Amount_6 = sb.Amount_6;
                    orps.EXP_CODE_7 = sb.EXP_CODE_7;
                    orps.Amount_7 = sb.Amount_7;
                    orps.EXP_CODE_8 = sb.EXP_CODE_8;
                    orps.Amount_8 = sb.Amount_8;
                    orps.EXP_CODE_9 = sb.EXP_CODE_9;
                    orps.Amount_9 = sb.Amount_9;
                    orps.AE = sb.AE;
                    orps.AF = sb.AF;
                    orps.AG = sb.AG;
                    orps.AH = sb.AH;
                    orps.AI = sb.AI;
                    try { db.SaveChanges(); } catch { }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if(sb.Date != null && sb.Particulars != null && sb.PAYEE != null)
                        {
                            ORSPS orps = new ORSPS();
                            orps.Row = sb.Row;
                            orps.Date = sb.Date;
                            orps.DB = sb.DB;
                            orps.PO = sb.PO;
                            orps.PR = sb.PR;
                            orps.PAYEE = sb.PAYEE;
                            orps.Adress = sb.Adress;
                            orps.Particulars = sb.Particulars;
                            orps.ORS_NO = sb.ORS_NO;
                            orps.FundSource = sb.FundSource;
                            orps.Gross = sb.Gross;
                            orps.EXP_CODE_1 = sb.EXP_CODE_1;
                            orps.Amount_1 = sb.Amount_1;
                            orps.EXP_CODE_2 = sb.EXP_CODE_2;
                            orps.Amount_2 = sb.Amount_2;
                            orps.EXP_CODE_3 = sb.EXP_CODE_3;
                            orps.Amount_3 = sb.Amount_3;
                            orps.EXP_CODE_4 = sb.EXP_CODE_4;
                            orps.Amount_4 = sb.Amount_4;
                            orps.EXP_CODE_5 = sb.EXP_CODE_5;
                            orps.Amount_5 = sb.Amount_5;
                            orps.EXP_CODE_6 = sb.EXP_CODE_6;
                            orps.Amount_6 = sb.Amount_6;
                            orps.EXP_CODE_7 = sb.EXP_CODE_7;
                            orps.Amount_7 = sb.Amount_7;
                            orps.EXP_CODE_8 = sb.EXP_CODE_8;
                            orps.Amount_8 = sb.Amount_8;
                            orps.EXP_CODE_9 = sb.EXP_CODE_9;
                            orps.Amount_9 = sb.Amount_9;
                            orps.AE = sb.AE;
                            orps.AF = sb.AF;
                            orps.AG = sb.AG;
                            orps.AH = sb.AH;
                            orps.AI = sb.AI;
                            orps.Created_By = User.Identity.GetUserName();
                            db.orsps.Add(orps);
                            
                            try { db.SaveChanges(); } catch { }
                        }
                        
                    }
                    catch { }
                }

            }
            return GetOrsPS();
        }

        [Route("delete/ors/ps",Name = "delete_ors_ps")]
        public String DeleteORSPS()
        {
            return "hahahehe";
        }
        [Route("print/orsps/{ID}",Name = "print_orsps")]
        public String PrintORSPS()
        {
            return "hahahehe";
        }

        [Route("ors/mooe",Name = "ors_mooe")]
        public ActionResult ORSMOOE()
        {
            ViewBag.Menu = "ORS | MOOE";
            return View();
        }
        [Route("get/ors/mooe",Name ="get_ors_mooe")]
        public JsonResult GetOrsMOOE()
        {
            var orsps = (from list in db.orsmooe
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
                             Gross = list.Gross,
                             EXP_CODE_1 = list.EXP_CODE_1,
                             Amount_1 = list.Amount_1,
                             EXP_CODE_2 = list.EXP_CODE_2,
                             Amount_2 = list.Amount_2,
                             EXP_CODE_3 = list.EXP_CODE_3,
                             Amount_3 = list.Amount_3,
                             EXP_CODE_4 = list.EXP_CODE_4,
                             Amount_4 = list.Amount_4,
                             EXP_CODE_5 = list.EXP_CODE_5,
                             Amount_5 = list.Amount_5,
                             EXP_CODE_6 = list.EXP_CODE_6,
                             Amount_6 = list.Amount_5,
                             EXP_CODE_7 = list.EXP_CODE_7,
                             Amount_7 = list.Amount_7,
                             EXP_CODE_8 = list.EXP_CODE_8,
                             Amount_8 = list.Amount_8,
                             EXP_CODE_9 = list.EXP_CODE_9,
                             Amount_9 = list.Amount_9,
                             AE = list.AE,
                             AF = list.AF,
                             AG = list.AG,
                             AH = list.AH,
                             AI = list.AI,
                             Created_By = list.Created_By
                         }).ToList();
            return Json(orsps, JsonRequestBehavior.AllowGet);
        }
        [Route("save/ors/mooe",Name ="save_ors_mooe")]
        public JsonResult SaveOrsMOOE(String data)
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
                    var orps = db.orsmooe.Where(p => p.ID == id).FirstOrDefault();
                    orps.Row = sb.Row;
                    orps.Date = sb.Date;
                    orps.DB = sb.DB;
                    orps.PO = sb.PO;
                    orps.PR = sb.PR;
                    orps.PAYEE = sb.PAYEE;
                    orps.Adress = sb.Adress;
                    orps.Particulars = sb.Particulars;
                    orps.ORS_NO = sb.ORS_NO;
                    orps.FundSource = sb.FundSource;
                    orps.Gross = sb.Gross;
                    orps.EXP_CODE_1 = sb.EXP_CODE_1;
                    orps.Amount_1 = sb.Amount_1;
                    orps.EXP_CODE_2 = sb.EXP_CODE_2;
                    orps.Amount_2 = sb.Amount_2;
                    orps.EXP_CODE_3 = sb.EXP_CODE_3;
                    orps.Amount_3 = sb.Amount_3;
                    orps.EXP_CODE_4 = sb.EXP_CODE_4;
                    orps.Amount_4 = sb.Amount_4;
                    orps.EXP_CODE_5 = sb.EXP_CODE_5;
                    orps.Amount_5 = sb.Amount_5;
                    orps.EXP_CODE_6 = sb.EXP_CODE_6;
                    orps.Amount_6 = sb.Amount_6;
                    orps.EXP_CODE_7 = sb.EXP_CODE_7;
                    orps.Amount_7 = sb.Amount_7;
                    orps.EXP_CODE_8 = sb.EXP_CODE_8;
                    orps.Amount_8 = sb.Amount_8;
                    orps.EXP_CODE_9 = sb.EXP_CODE_9;
                    orps.Amount_9 = sb.Amount_9;
                    orps.AE = sb.AE;
                    orps.AF = sb.AF;
                    orps.AG = sb.AG;
                    orps.AH = sb.AH;
                    orps.AI = sb.AI;
                    try { db.SaveChanges(); } catch { }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if (sb.Date != null && sb.Particulars != null && sb.PAYEE != null)
                        {
                            ORSMOOE orsmooe = new ORSMOOE();
                            orsmooe.Row = sb.Row;
                            orsmooe.Date = sb.Date;
                            orsmooe.DB = sb.DB;
                            orsmooe.PO = sb.PO;
                            orsmooe.PR = sb.PR;
                            orsmooe.PAYEE = sb.PAYEE;
                            orsmooe.Adress = sb.Adress;
                            orsmooe.Particulars = sb.Particulars;
                            orsmooe.ORS_NO = sb.ORS_NO;
                            orsmooe.FundSource = sb.FundSource;
                            orsmooe.Gross = sb.Gross;
                            orsmooe.EXP_CODE_1 = sb.EXP_CODE_1;
                            orsmooe.Amount_1 = sb.Amount_1;
                            orsmooe.EXP_CODE_2 = sb.EXP_CODE_2;
                            orsmooe.Amount_2 = sb.Amount_2;
                            orsmooe.EXP_CODE_3 = sb.EXP_CODE_3;
                            orsmooe.Amount_3 = sb.Amount_3;
                            orsmooe.EXP_CODE_4 = sb.EXP_CODE_4;
                            orsmooe.Amount_4 = sb.Amount_4;
                            orsmooe.EXP_CODE_5 = sb.EXP_CODE_5;
                            orsmooe.Amount_5 = sb.Amount_5;
                            orsmooe.EXP_CODE_6 = sb.EXP_CODE_6;
                            orsmooe.Amount_6 = sb.Amount_6;
                            orsmooe.EXP_CODE_7 = sb.EXP_CODE_7;
                            orsmooe.Amount_7 = sb.Amount_7;
                            orsmooe.EXP_CODE_8 = sb.EXP_CODE_8;
                            orsmooe.Amount_8 = sb.Amount_8;
                            orsmooe.EXP_CODE_9 = sb.EXP_CODE_9;
                            orsmooe.Amount_9 = sb.Amount_9;
                            orsmooe.AE = sb.AE;
                            orsmooe.AF = sb.AF;
                            orsmooe.AG = sb.AG;
                            orsmooe.AH = sb.AH;
                            orsmooe.AI = sb.AI;
                            orsmooe.Created_By = User.Identity.GetUserName();
                            db.orsmooe.Add(orsmooe);
                            try { db.SaveChanges(); } catch { }
                        }

                    }
                    catch { }
                }

            }
            return GetOrsMOOE();
        }
        [Route("delete/ors/mooe",Name ="delete_ors_mooe")]
        public String DeleteORSMOOE()
        {
            return "";
        }
    }
}