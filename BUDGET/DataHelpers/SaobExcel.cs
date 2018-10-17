using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
namespace BUDGET
{
    public class SaobExcel
    {
        public void CreateExcel()
        {
            BudgetDB db = new BudgetDB();
            FileInfo excelFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB.xlsx"));
            var excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB.xlsx"));
            Excel.Worksheet worksheet = workbook.Sheets[1];
            Excel.Range range = worksheet.UsedRange;

            Int32 startRow = 15;

            var allotments = db.allotments.ToList();

            foreach(var allotment in allotments)
            {
                range.Cells[startRow, 1] = allotment.Title.ToUpper().ToString();
                startRow++;
            }
            

            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(range);
            Marshal.ReleaseComObject(worksheet);
            workbook.Close();
            Marshal.ReleaseComObject(workbook);

            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);

        }
    }
}