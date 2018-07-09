
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
    public class UploadsController : Controller
    {
        BudgetDB db = new BudgetDB();
        JsonGetter jg = new JsonGetter();
        // GET: Uploads
        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult UploadUacs(HttpPostedFileBase file)
        {
            String filename = file.FileName;
            String contentType = file.ContentType;
            byte[] filebytes = new byte[file.ContentLength];
            var data = file.InputStream.Read(filebytes, 0, Convert.ToInt32(file.ContentLength));
            using (var package = new ExcelPackage(file.InputStream))
            {
                var worksheet = package.Workbook.Worksheets[1];
                var noOfCol = worksheet.Dimension.End.Column;
                var noOfRow = worksheet.Dimension.End.Row;
                //GAA
                int row = 1;
                for (int i = 2; i < noOfRow; i++)
                {
                    UACS uacs = new UACS();
                    try
                    {
                        uacs.Title = worksheet.Cells[i, 1].Value.ToString();
                        uacs.Code = worksheet.Cells[i, 2].Value.ToString();
                        uacs.Line = row++;
                        db.uacs.Add(uacs);
                    }
                    catch { }
                }
                db.SaveChanges();
                // MOOE
                            }
            return RedirectToAction("ExpenseCodes", "ExpenseCodes");
        }

        public ActionResult UploadPrexc()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult UploadPrexc(HttpPostedFileBase file)
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
                int row = 1;
                for (int i = 0; i < noOfRow; i++)
                {
                    PREXC prexc = new PREXC();
                    try
                    {
                        prexc.Desc = worksheet.Cells[i, 1].Value.ToString();
                        prexc.Code1 = worksheet.Cells[i, 2].Value.ToString();
                        prexc.Code2 = worksheet.Cells[i, 3].Value.ToString();
                        prexc.Line = row++;
                        db.prexc.Add(prexc);

                    }
                    catch { }
                }
                db.SaveChanges();
                // MOOE
            }
            return RedirectToAction("Prexc", "ExpenseCodes");
        }

        public ActionResult UploadORSMooe()
        {
            return PartialView();
        }

        public String SaveORSMooe(HttpPostedFileBase file)
        {

            String filename = file.FileName;
            MemoryStream mem = new MemoryStream();
            mem.SetLength((int)file.ContentLength);
            file.InputStream.Read(mem.GetBuffer(), 0, (int)file.ContentLength);

            using (var package = new ExcelPackage(mem))
            {
                var worksheet = package.Workbook.Worksheets[1];
                var noOfCol = worksheet.Dimension.End.Column;
                var noOfRow = worksheet.Dimension.End.Row;
                String date = "";
                //GAA
                int row = 1;
                for (int i = 7; i < noOfRow; i++)
                {
                    ORS ors = new ORS();
                    try
                    {
                        ors.is_obligated = "OBLIGATED";

                        date = worksheet.Cells[i, 2].Value.ToString();
                        DateTime datetime = Convert.ToDateTime(date);
                        try { ors.Date = datetime; } catch { ors.Date = Convert.ToDateTime("01/01/1900"); }
                        try { ors.Date1 = datetime.ToString("MM/dd/yyyy"); } catch { ors.Date1 = ""; }
                        try { ors.DB = worksheet.Cells[i, 3].Value.ToString(); } catch { ors.DB = ""; }
                        try { ors.PO = worksheet.Cells[i, 4].Value.ToString(); } catch { ors.PO = ""; }
                        try { ors.PR = worksheet.Cells[i, 5].Value.ToString(); } catch { ors.PR = ""; }
                        try { ors.PAYEE = worksheet.Cells[i, 6].Value.ToString(); } catch { ors.PAYEE = ""; }
                        try { ors.Adress = worksheet.Cells[i, 7].Value.ToString(); } catch { ors.Adress = ""; }
                        try { ors.Particulars = worksheet.Cells[i, 8].Value.ToString(); } catch { ors.Particulars = ""; }
                        ors.ors_id = 1003;
                        ors.Date_Added = DateTime.Now.Date;
                        ors.dateadded = DateTime.Now.Date.ToString();
                        ors.FundSource = "STO-OPERATIONS";
                        ors.head_requesting_office = "DR. EMILIA MONICIMPO";
                        ors.Created_By = "doh7budget";
                        db.ors.Add(ors);
                        db.SaveChanges();
                    }
                    catch { }      
                        
                    
                }
                
            }
            return "ok";
        }
    }
}