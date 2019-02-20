using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using OfficeOpenXml;
using System.Data;
using System.Web.Mvc;
namespace BUDGET
{
    public class ThreadOrsUpload
    {
        
        public static void WorkSheet(object mem)
        {
            BudgetDB db = new BudgetDB();
            MemoryStream newMem = (MemoryStream)mem;
            using (var package = new ExcelPackage(newMem))
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
                        ors.allotment = 1003;
                        ors.Date_Added = DateTime.Now.Date;
                        ors.dateadded = DateTime.Now.Date.ToString();
                        ors.FundSource = "PHM";
                        ors.Created_By = "doh7budget";
                        db.ors.Add(ors);
                        db.SaveChanges();
                    }
                    catch { }
                }
            }
        }
    }
}