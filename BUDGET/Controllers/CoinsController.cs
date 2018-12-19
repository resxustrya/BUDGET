using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BUDGET
{
    [AllowAnonymous]
    public class CoinsController : Controller
    {
        // GET: Coins
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase coins)
        {
            String filename = coins.FileName;
            String contentType = coins.ContentType;
            byte[] filebytes = new byte[coins.ContentLength];

            
            var data = coins.InputStream.Read(filebytes, 0, Convert.ToInt32(coins.ContentLength));
            using (var package = new ExcelPackage(coins.InputStream))
            {
                var worksheet = package.Workbook.Worksheets[1];
                var noOfCol = worksheet.Dimension.End.Column;
                var noOfRow = worksheet.Dimension.End.Row;
                //GAA
                int row = 1;
                for (int i = 2; i < noOfRow; i++)
                {
                }
                // MOOE
            }
            return RedirectToAction("Index");
        }
    }
}