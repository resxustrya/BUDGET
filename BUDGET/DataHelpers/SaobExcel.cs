using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using System.IO;
namespace BUDGET
{
    public class SaobExcel
    {
        public void CreateExcel()
        {
            FileInfo excelFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB.xlsx"));
            using (ExcelPackage excel = new ExcelPackage(excelFile))
            {
                ExcelWorkbook workbook = excel.Workbook;
                ExcelWorksheet worksheet = workbook.Worksheets.First();

                worksheet.Cells["A16"].Value = "PERSONNEL SERVICES";
                excel.Save();
            }
        }
    }
}