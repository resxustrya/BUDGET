using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;
using BUDGET.Models;
using Microsoft.Office.Core;
namespace BUDGET.Controllers
{
   
    public class FormsController : Controller
    {
        // GET: Forms
        private BudgetDB db = new BudgetDB();
        public ActionResult Form1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadForm1(HttpPostedFileBase file)
        {
            List<User> userlist = new List<User>();
            try
            {
                if(file.ContentLength > 0)
                {
                    String filename = file.FileName;
                    String contentType = file.ContentType;
                    byte[] filebytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(filebytes, 0, Convert.ToInt32(file.ContentLength));
                    
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var worksheet = currentSheet.First();
                        var noOfCol = worksheet.Dimension.End.Column;
                        var noOfRow = worksheet.Dimension.End.Row;
                        for(int i = 1; i <= noOfRow; i++)
                        {
                            var user = new User();
                            user.FirstName = worksheet.Cells[i, 1].Value.ToString();
                            user.MiddleName = worksheet.Cells[i, 2].Value.ToString();
                            user.LastName = worksheet.Cells[i, 3].Value.ToString();
                            db.users.Add(user);
                        }
                        db.SaveChanges();
                    }
                }
            }catch(Exception err)
            {
                
            }
            return RedirectToAction("Index", "Home");
        }
    }
}