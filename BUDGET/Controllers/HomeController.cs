
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
    [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
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
       
        public ActionResult UploadGaa()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult UploadGaaPost(HttpPostedFileBase file)
        {
           
            String filename = file.FileName;
            String contentType = file.ContentType;
            byte[] filebytes = new byte[file.ContentLength];
            var data = file.InputStream.Read(filebytes, 0, Convert.ToInt32(file.ContentLength));
            using (var package = new ExcelPackage(file.InputStream))
            {
                var worksheet = package.Workbook.Worksheets[2];
                var noOfCol = worksheet.Dimension.End.Column;
                var noOfRow = worksheet.Dimension.End.Row;
                //GAA
                int gaaLastLine = jg.GetLastLine("PS");
                for (int i = 7; i < noOfRow; i++)
                {
                    PersonnelServices ps = new PersonnelServices();
                    try
                    {
                        ps.Particulars = worksheet.Cells[i, 1].Value.ToString();
                        try { ps.UACS = worksheet.Cells[i, 2].Value.ToString(); } catch { ps.UACS = null; }
                        try { ps.STO_Operations = Convert.ToDouble(worksheet.Cells[i, 3].Value.ToString().Replace(",", "")); } catch { ps.STO_Operations = 0; }
                        try { ps.PHM = Convert.ToDouble(worksheet.Cells[i, 6].Value.ToString().Replace(",", "")); } catch { ps.PHM = 0; }
                        try { ps.RRHFS = Convert.ToDouble(worksheet.Cells[i, 9].Value.ToString().Replace(",", "")); } catch { ps.RRHFS = 0; }
                        ps.Line = gaaLastLine;
                        db.ps.Add(ps);
                        gaaLastLine++;
                    }
                    catch { }
                }
                db.SaveChanges();
                // MOOE
                worksheet = package.Workbook.Worksheets[3];
                noOfCol = worksheet.Dimension.End.Column;
                noOfRow = worksheet.Dimension.End.Row;

                int mooeLastLine = jg.GetLastLine("MOOE");
                for (int i = 7; i < noOfRow; i++)
                {
                    MOOEs mooe = new MOOEs();
                    try
                    {
                        mooe.Paraticulars = worksheet.Cells[i, 1].Value.ToString();
                        try { mooe.UACS = worksheet.Cells[i, 2].Value.ToString(); } catch { mooe.UACS = ""; }
                        try { mooe.STO_Operations = Convert.ToDouble(worksheet.Cells[i, 3].Value.ToString().Replace(",", "")); } catch { mooe.STO_Operations = 0; }
                        try { mooe.PHM = Convert.ToDouble(worksheet.Cells[i, 6].Value.ToString().Replace(",", "")); } catch { mooe.PHM = 0; }
                        try { mooe.RRHFS = Convert.ToDouble(worksheet.Cells[i, 9].Value.ToString().Replace(",", "")); } catch { mooe.RRHFS = 0; }
                        try { mooe.HSRD = Convert.ToDouble(worksheet.Cells[i, 12].Value.ToString().Replace(",", "")); } catch { mooe.HSRD = 0; }
                        try { mooe.LHSDA = Convert.ToDouble(worksheet.Cells[i, 13].Value.ToString().Replace(",", "")); } catch { mooe.LHSDA = 0; }
                        try { mooe.HRHICM = Convert.ToDouble(worksheet.Cells[i, 14].Value.ToString().Replace(",", "")); } catch { mooe.HRHICM = 0; }
                        try { mooe.HRDP = Convert.ToDouble(worksheet.Cells[i,15].Value.ToString().Replace(",", "")); } catch { mooe.HRDP = 0; }
                        try { mooe.HP = Convert.ToDouble(worksheet.Cells[i, 17].Value.ToString().Replace(",", "")); } catch { mooe.HP = 0; }
                        try { mooe.ES = Convert.ToDouble(worksheet.Cells[i, 18].Value.ToString().Replace(",", "")); } catch { mooe.ES = 0; }
                        try { mooe.HEPR = Convert.ToDouble(worksheet.Cells[i, 19].Value.ToString().Replace(",", "")); } catch { mooe.HEPR = 0; }
                        mooe.Line = mooeLastLine;
                        db.mooe.Add(mooe);
                        mooeLastLine++;
                    }
                    catch {  }
                }
                db.SaveChanges();
            }
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
        [HttpPost]
        [Route("delete/json/mooe",Name ="delete_json_mooe")]
        public ActionResult DeleteMOOE(String data)
        {
            try
            {
                dynamic mooe = JsonConvert.DeserializeObject<dynamic>(data);
                int ID = Convert.ToInt32(mooe.ID);
                var del_mooe = db.mooe.Where(p => p.ID == ID).FirstOrDefault();
                db.mooe.Remove(del_mooe);
                db.SaveChanges();
            }catch(Exception ex)
            {

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [Route("get/json/mooe",Name = "get_json_mooe")]
        public JsonResult GetMOOE()
        {
            var mooe = (from list in db.mooe orderby list.Line ascending
                      select new
                      {
                          ID = list.ID,
                          Line = list.Line,
                          Paraticulars = list.Paraticulars,
                          UACS = list.UACS,
                          STO_Operations = list.STO_Operations,
                          PHM = list.PHM,
                          RRHFS = list.RRHFS,
                          HSRD = list.HSRD,
                          HRHICM = list.HRHICM,
                          HRDP = list.HRDP,
                          HP = list.HP,
                          ES = list.ES,
                          HEPR = list.HEPR
                      }).ToList();
            return Json(mooe, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Route("save/json/mooe",Name = "save_json_mooe")]
        public JsonResult SaveMOOE(String data,String deleted)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            foreach (Object s in list)
            {
                try
                {
                    MOOEs mooe = JsonConvert.DeserializeObject<MOOEs>(s.ToString());
                    var temp = db.mooe.Where(p => p.ID == mooe.ID).FirstOrDefault();
                    temp.Line = mooe.Line;
                    temp.Paraticulars = mooe.Paraticulars;
                    temp.UACS = mooe.UACS;
                    temp.STO_Operations = mooe.STO_Operations;
                    temp.PHM = mooe.PHM;
                    temp.RRHFS = mooe.RRHFS;
                    temp.HSRD = mooe.HSRD;
                    temp.HRHICM = mooe.HRHICM;
                    temp.HRDP = mooe.HRDP;
                    temp.HP = mooe.HP;
                    temp.ES = mooe.ES;
                    temp.HEPR = mooe.HEPR;
                    try { db.SaveChanges(); } catch { }
                }
                catch (Exception ex)
                {
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if (o.ID == null && o.Paraticulars != null && o.UACS != null)
                        {
                            MOOEs new_mooe = new MOOEs();
                            new_mooe.Line = o.Line;
                            new_mooe.Paraticulars = o.Paraticulars;
                            new_mooe.UACS = o.UACS;
                            new_mooe.STO_Operations = o.STO_Operations;
                            new_mooe.PHM = o.PHM;
                            new_mooe.RRHFS = o.RRHFS;
                            new_mooe.HSRD = o.HSRD;
                            new_mooe.HRHICM = o.HRHICM;
                            new_mooe.HRDP = o.HRDP;
                            new_mooe.HP = o.HP;
                            new_mooe.ES = o.ES;
                            new_mooe.HEPR = o.HEPR;
                            db.mooe.Add(new_mooe);
                            db.SaveChanges();
                        }
                    }
                    catch { }
                    
                }
            }
            return GetMOOE();
        }
        public ActionResult GetMOOEList()
        {
            return Json(jg.GetMOOE(), JsonRequestBehavior.AllowGet);
        }
        public FileContentResult Report()
        {
            ExcelDataExport report = new ExcelDataExport();
            string contenttype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            byte[] filecontent = report.MOOE();
            return File(filecontent, contenttype, "MOOE.xlsx");

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