
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using OfficeOpenXml;
using System.Data;
using System.Web.Mvc;
using BUDGET.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Data.Entity;
using BUDGET.DataHelpers;
using BUDGET.Filters;
namespace BUDGET.Controllers
{
    [Authorize]
    [YearlyFilter]
    [OutputCache(Duration = 0)]
   
    public class HomeController : Controller
    {
        BudgetDB db = new BudgetDB();
        JsonGetter jg = new JsonGetter();
        public int PageSize = 10;
        public ActionResult Index()
        {
            return RedirectToRoute("GAA_Personnel_Services");
        }
        
        [AllowAnonymous]
        public ActionResult Handson()
        {
            return RedirectToRoute("GAA_Personnel_Services");
        }
       
        
        [Route("gaa/personel-services",Name = "GAA_Personnel_Services")]
        public ActionResult GaaPersonelServices()
        {
            ViewBag.Menu = GlobalData.Year + " GAA | PERSONNEL SERVICES";
            return View();
        }
        [Route("get/gaa/personnel-services",Name = "get_json_gaa")]
        public JsonResult GetPersonelGaa()
        {
            var ps = (from list in db.ps orderby list.Line ascending
                      select new
                      {
                          ID = list.ID,
                          Line = list.Line,
                          Particulars = list.Particulars,
                          UACS = list.UACS,
                          STO_Operations = list.STO_Operations,
                          PHM = list.PHM,
                          RRHFS = list.RRHFS,
                          Total = list.STO_Operations + list.PHM + list.RRHFS
                      }).ToList();
            return Json(ps, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Route("save/gaa/personnel-services", Name = "save_json_gaa")]
        public JsonResult SavePersonelGaa(String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            foreach (Object s in list)
            {
                try
                {
                    PersonnelServices sb = JsonConvert.DeserializeObject<PersonnelServices>(s.ToString());
                    var ps = db.ps.Where(p => p.ID == sb.ID).FirstOrDefault();
                    ps.Line = sb.Line;
                    ps.Particulars = sb.Particulars;
                    ps.UACS = sb.UACS;
                    ps.PHM = sb.PHM;
                    ps.RRHFS = sb.RRHFS;
                    ps.STO_Operations = sb.STO_Operations;
                    try { db.SaveChanges(); } catch { }
                }catch(Exception ex)
                {
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if (o.ID == null && o.Particulars != null && o.UACS != null)
                        {
                            PersonnelServices ps_new = new PersonnelServices();
                            ps_new.Line = o.Line;
                            ps_new.Particulars = o.Particulars;
                            ps_new.UACS = o.UACS;
                            ps_new.PHM = o.PHM;
                            ps_new.RRHFS = o.RRHFS;
                            ps_new.STO_Operations = o.STO_Operations;
                            db.ps.Add(ps_new);
                            db.SaveChanges();
                        }
                    }
                    catch { }
                }
                
            }
            return GetPersonelGaa();
        }
        [HttpPost]
        [Route("delete/json/gaa",Name = "delete_json_gaa")]
        public ActionResult DeleteGaa(String data)
        {
            try
            {
                dynamic sb = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(sb.ID);
                var ps = db.ps.Where(p => p.ID == ID).FirstOrDefault();
                db.ps.Remove(ps);
                db.SaveChanges();
            }
            catch(Exception ex)
            {

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MOOE()
        {
            ViewBag.Menu = GlobalData.Year + " GAA MOOE";
            return View();
        }
        
     
       
        public FileContentResult GAA()
        {
            ExcelDataExport report = new ExcelDataExport();
            string contenttype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            byte[] filecontent = report.ReadReports();
            return File(filecontent, contenttype, "MOOE.xlsx");

        }
        public ActionResult TEST()
        {
            return View();
        }
    }
}