﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using OfficeOpenXml;
using System.Data;
using System.Web.Mvc;
using System.Threading;
namespace BUDGET.Controllers
{
    [Authorize(Roles = "Admin")]
    [YearlyFilter]
    [OutputCache(Duration = 0)]
    public class UploadsController : Controller
    {
        BudgetDB db = new BudgetDB();
       
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

        public ActionResult UploadORS()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public String UploadORS(HttpPostedFileBase file)
        {
            String filename = file.FileName;
            MemoryStream mem = new MemoryStream();
            mem.SetLength((int)file.ContentLength);
            file.InputStream.Read(mem.GetBuffer(), 0, (int)file.ContentLength);

            Thread thread1 = new Thread(ThreadOrsUpload.WorkSheet);
            thread1.Start(mem);

            return "ok";
        }
    }
}