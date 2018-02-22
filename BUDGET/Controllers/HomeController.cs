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
namespace BUDGET.Controllers
{
    [AllowAnonymous]
    [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        BudgetDB db = new BudgetDB();
        JsonGetter jg = new JsonGetter();
        public int PageSize = 10;
        [AllowAnonymous]
        public ActionResult Index(int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                userslist = db.users.OrderBy(p => p.ID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.users.Count()
                }
            };
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            User user = db.users.Find(id);
            if(user != null)
            {
                Session["edit_user"] = user.ID;
                return View(user);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(FormCollection formcollection)
        {
            String firtname = formcollection["firstname"].ToString();
            String middlename = formcollection["middlename"].ToString();
            String lastname = formcollection["lastname"].ToString();
            int id = (int)Session["edit_user"];
            var user = (from u in db.users where u.ID == id select u).FirstOrDefault();
            
            user.FirstName = firtname;
            user.MiddleName = middlename;
            user.LastName = lastname;
            try
            {
                db.SaveChanges();
            }catch(Exception ex)
            {

            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            User user = db.users.Find(id);
            if(user != null)
            {
                db.users.Remove(user);
                db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult About() 
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
                int line = jg.GetLastLine("PS");
                for(int i = 7; i < noOfRow; i++)
                {
                    PersonnelServices ps = new PersonnelServices();
                    try
                    {
                        ps.Particulars = worksheet.Cells[i, 1].Value.ToString();
                        try { ps.UACS = worksheet.Cells[i, 2].Value.ToString(); } catch { ps.UACS = null; }
                        try { ps.STO_Operations = Convert.ToDouble(worksheet.Cells[i, 3].Value.ToString().Replace(",", "")); } catch { ps.STO_Operations = 0; }
                        try { ps.PHM = Convert.ToDouble(worksheet.Cells[i, 6].Value.ToString().Replace(",", "")); } catch { ps.PHM = 0; }
                        try { ps.RRHFS = Convert.ToDouble(worksheet.Cells[i, 9].Value.ToString().Replace(",", "")); } catch { ps.RRHFS = 0; }
                        ps.Line = 
                        db.ps.Add(ps);
                    }
                    catch { }
                }
                db.SaveChanges();
                // MOOE
                worksheet = package.Workbook.Worksheets[3];
                noOfCol = worksheet.Dimension.End.Column;
                noOfRow = worksheet.Dimension.End.Row;

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
                        db.mooe.Add(mooe);
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
            return View();
        }
        [Route("get/gaa/personnel-services",Name = "get_json_gaa")]
        public JsonResult GetPersonelGaa()
        {
            var ps = (from list in db.ps
                      select new
                      {
                          ID = list.ID,
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
                PersonnelServices sb = JsonConvert.DeserializeObject<PersonnelServices>(s.ToString());
                var ps = db.ps.Where(p => p.ID == sb.ID).FirstOrDefault();
                ps.Particulars = sb.Particulars;
                ps.UACS = sb.UACS;
                ps.PHM = sb.PHM;
                ps.RRHFS = sb.RRHFS;
                ps.STO_Operations = sb.STO_Operations;
                try { db.SaveChanges(); } catch { }
            }
            var ok = new { ok = true };
            return Json(ok, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MOOE()
        {
            return View();
        }
        [Route("get/json/mooe",Name = "get_json_mooe")]
        public JsonResult GetMOOE()
        {
            var mooe = (from list in db.mooe
                      select new
                      {
                          ID = list.ID,
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
    }
}