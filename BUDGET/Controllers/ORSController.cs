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
        public ActionResult DeleteORSPS(String data)
        {
            try
            {
                dynamic orsps = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(orsps.ID);
                var del_orsps = db.orsps.Where(p => p.ID == ID).FirstOrDefault();
                db.orsps.Remove(del_orsps);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return Json(true, JsonRequestBehavior.AllowGet);
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
        public ActionResult DeleteORSMOOE(String data)
        {
            try
            {
                dynamic orsps = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(orsps.ID);
                var del_orsmooe = db.orsmooe.Where(p => p.ID == ID).FirstOrDefault();
                db.orsmooe.Remove(del_orsmooe);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        
        [Route("ors/vtf",Name = "ors_vtf")]
        public ActionResult ORSVTF()
        {
            ViewBag.Menu = "ORS | VTF";
            return View();
        }
        [Route("get/ors/vtf",Name ="get_ors_vtf")]
        public ActionResult GETORSVTF()
        {
            var orsvtf = (from list in db.orsvtf
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
            return Json(orsvtf, JsonRequestBehavior.AllowGet);
        }
        [Route("save/ors/vtf",Name ="save_ors_vtf")]
        public ActionResult SaveORSVtf(String data)
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
                    var orsvtf = db.orsvtf.Where(p => p.ID == id).FirstOrDefault();
                    orsvtf.Row = sb.Row;
                    orsvtf.Date = sb.Date;
                    orsvtf.DB = sb.DB;
                    orsvtf.PO = sb.PO;
                    orsvtf.PR = sb.PR;
                    orsvtf.PAYEE = sb.PAYEE;
                    orsvtf.Adress = sb.Adress;
                    orsvtf.Particulars = sb.Particulars;
                    orsvtf.ORS_NO = sb.ORS_NO;
                    orsvtf.FundSource = sb.FundSource;
                    orsvtf.Gross = sb.Gross;
                    orsvtf.EXP_CODE_1 = sb.EXP_CODE_1;
                    orsvtf.Amount_1 = sb.Amount_1;
                    orsvtf.EXP_CODE_2 = sb.EXP_CODE_2;
                    orsvtf.Amount_2 = sb.Amount_2;
                    orsvtf.EXP_CODE_3 = sb.EXP_CODE_3;
                    orsvtf.Amount_3 = sb.Amount_3;
                    orsvtf.EXP_CODE_4 = sb.EXP_CODE_4;
                    orsvtf.Amount_4 = sb.Amount_4;
                    orsvtf.EXP_CODE_5 = sb.EXP_CODE_5;
                    orsvtf.Amount_5 = sb.Amount_5;
                    orsvtf.EXP_CODE_6 = sb.EXP_CODE_6;
                    orsvtf.Amount_6 = sb.Amount_6;
                    orsvtf.EXP_CODE_7 = sb.EXP_CODE_7;
                    orsvtf.Amount_7 = sb.Amount_7;
                    orsvtf.EXP_CODE_8 = sb.EXP_CODE_8;
                    orsvtf.Amount_8 = sb.Amount_8;
                    orsvtf.EXP_CODE_9 = sb.EXP_CODE_9;
                    orsvtf.Amount_9 = sb.Amount_9;
                    orsvtf.AE = sb.AE;
                    orsvtf.AF = sb.AF;
                    orsvtf.AG = sb.AG;
                    orsvtf.AH = sb.AH;
                    orsvtf.AI = sb.AI;
                    try { db.SaveChanges(); } catch { }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if (sb.Date != null && sb.Particulars != null && sb.PAYEE != null)
                        {
                            ORSVTF orsvtf = new ORSVTF();
                            orsvtf.Row = sb.Row;
                            orsvtf.Date = sb.Date;
                            orsvtf.DB = sb.DB;
                            orsvtf.PO = sb.PO;
                            orsvtf.PR = sb.PR;
                            orsvtf.PAYEE = sb.PAYEE;
                            orsvtf.Adress = sb.Adress;
                            orsvtf.Particulars = sb.Particulars;
                            orsvtf.ORS_NO = sb.ORS_NO;
                            orsvtf.FundSource = sb.FundSource;
                            orsvtf.Gross = sb.Gross;
                            orsvtf.EXP_CODE_1 = sb.EXP_CODE_1;
                            orsvtf.Amount_1 = sb.Amount_1;
                            orsvtf.EXP_CODE_2 = sb.EXP_CODE_2;
                            orsvtf.Amount_2 = sb.Amount_2;
                            orsvtf.EXP_CODE_3 = sb.EXP_CODE_3;
                            orsvtf.Amount_3 = sb.Amount_3;
                            orsvtf.EXP_CODE_4 = sb.EXP_CODE_4;
                            orsvtf.Amount_4 = sb.Amount_4;
                            orsvtf.EXP_CODE_5 = sb.EXP_CODE_5;
                            orsvtf.Amount_5 = sb.Amount_5;
                            orsvtf.EXP_CODE_6 = sb.EXP_CODE_6;
                            orsvtf.Amount_6 = sb.Amount_6;
                            orsvtf.EXP_CODE_7 = sb.EXP_CODE_7;
                            orsvtf.Amount_7 = sb.Amount_7;
                            orsvtf.EXP_CODE_8 = sb.EXP_CODE_8;
                            orsvtf.Amount_8 = sb.Amount_8;
                            orsvtf.EXP_CODE_9 = sb.EXP_CODE_9;
                            orsvtf.Amount_9 = sb.Amount_9;
                            orsvtf.AE = sb.AE;
                            orsvtf.AF = sb.AF;
                            orsvtf.AG = sb.AG;
                            orsvtf.AH = sb.AH;
                            orsvtf.AI = sb.AI;
                            orsvtf.Created_By = User.Identity.GetUserName();
                            db.orsvtf.Add(orsvtf);
                            try { db.SaveChanges(); } catch { }
                        }

                    }
                    catch { }
                }
            }
            return GETORSVTF();
        }


        [Route("delete/ors/vtf",Name ="delete_ors_vtf")]
        public ActionResult  DeleteORSVtf(String data)
        {
            try
            {
                dynamic orsvtf = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(orsvtf.ID);
                var del_orsvtf = db.orsvtf.Where(p => p.ID == ID).FirstOrDefault();
                db.orsvtf.Remove(del_orsvtf);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [Route("ors/co", Name = "ors_co")]
        public ActionResult ORSCO()
        {
            ViewBag.Menu = "ORS | CO";
            return View();
        }
        
        [Route("get/ors/co",Name ="get_ors_co")]
        public ActionResult GetORSCO()
        {
            var orsco = (from list in db.orsco
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
            return Json(orsco, JsonRequestBehavior.AllowGet);
        }
        [Route("save/ors/co",Name ="save_ors_co")]
        public ActionResult SaveORSCO(String data)
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
                    var orsco = db.orsco.Where(p => p.ID == id).FirstOrDefault();
                    orsco.Row = sb.Row;
                    orsco.Date = sb.Date;
                    orsco.DB = sb.DB;
                    orsco.PO = sb.PO;
                    orsco.PR = sb.PR;
                    orsco.PAYEE = sb.PAYEE;
                    orsco.Adress = sb.Adress;
                    orsco.Particulars = sb.Particulars;
                    orsco.ORS_NO = sb.ORS_NO;
                    orsco.FundSource = sb.FundSource;
                    orsco.Gross = sb.Gross;
                    orsco.EXP_CODE_1 = sb.EXP_CODE_1;
                    orsco.Amount_1 = sb.Amount_1;
                    orsco.EXP_CODE_2 = sb.EXP_CODE_2;
                    orsco.Amount_2 = sb.Amount_2;
                    orsco.EXP_CODE_3 = sb.EXP_CODE_3;
                    orsco.Amount_3 = sb.Amount_3;
                    orsco.EXP_CODE_4 = sb.EXP_CODE_4;
                    orsco.Amount_4 = sb.Amount_4;
                    orsco.EXP_CODE_5 = sb.EXP_CODE_5;
                    orsco.Amount_5 = sb.Amount_5;
                    orsco.EXP_CODE_6 = sb.EXP_CODE_6;
                    orsco.Amount_6 = sb.Amount_6;
                    orsco.EXP_CODE_7 = sb.EXP_CODE_7;
                    orsco.Amount_7 = sb.Amount_7;
                    orsco.EXP_CODE_8 = sb.EXP_CODE_8;
                    orsco.Amount_8 = sb.Amount_8;
                    orsco.EXP_CODE_9 = sb.EXP_CODE_9;
                    orsco.Amount_9 = sb.Amount_9;
                    orsco.AE = sb.AE;
                    orsco.AF = sb.AF;
                    orsco.AG = sb.AG;
                    orsco.AH = sb.AH;
                    orsco.AI = sb.AI;
                    try { db.SaveChanges(); } catch { }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if (sb.Date != null && sb.Particulars != null && sb.PAYEE != null)
                        {
                            ORSCO orsco = new ORSCO();
                            orsco.Row = sb.Row;
                            orsco.Date = sb.Date;
                            orsco.DB = sb.DB;
                            orsco.PO = sb.PO;
                            orsco.PR = sb.PR;
                            orsco.PAYEE = sb.PAYEE;
                            orsco.Adress = sb.Adress;
                            orsco.Particulars = sb.Particulars;
                            orsco.ORS_NO = sb.ORS_NO;
                            orsco.FundSource = sb.FundSource;
                            orsco.Gross = sb.Gross;
                            orsco.EXP_CODE_1 = sb.EXP_CODE_1;
                            orsco.Amount_1 = sb.Amount_1;
                            orsco.EXP_CODE_2 = sb.EXP_CODE_2;
                            orsco.Amount_2 = sb.Amount_2;
                            orsco.EXP_CODE_3 = sb.EXP_CODE_3;
                            orsco.Amount_3 = sb.Amount_3;
                            orsco.EXP_CODE_4 = sb.EXP_CODE_4;
                            orsco.Amount_4 = sb.Amount_4;
                            orsco.EXP_CODE_5 = sb.EXP_CODE_5;
                            orsco.Amount_5 = sb.Amount_5;
                            orsco.EXP_CODE_6 = sb.EXP_CODE_6;
                            orsco.Amount_6 = sb.Amount_6;
                            orsco.EXP_CODE_7 = sb.EXP_CODE_7;
                            orsco.Amount_7 = sb.Amount_7;
                            orsco.EXP_CODE_8 = sb.EXP_CODE_8;
                            orsco.Amount_8 = sb.Amount_8;
                            orsco.EXP_CODE_9 = sb.EXP_CODE_9;
                            orsco.Amount_9 = sb.Amount_9;
                            orsco.AE = sb.AE;
                            orsco.AF = sb.AF;
                            orsco.AG = sb.AG;
                            orsco.AH = sb.AH;
                            orsco.AI = sb.AI;
                            orsco.Created_By = User.Identity.GetUserName();
                            db.orsco.Add(orsco);
                            try { db.SaveChanges(); } catch { }
                        }

                    }
                    catch { }
                }
            }
            return GetORSCO();
        }
        [Route("delete/ors/co",Name ="delete_ors_co")]
        public ActionResult DeleteORSCO(String data)
        {
            try
            {
                dynamic orsco = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(orsco.ID);
                var del_orsco = db.orsco.Where(p => p.ID == ID).FirstOrDefault();
                db.orsco.Remove(del_orsco);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}