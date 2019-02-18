using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace BUDGET
{
    public class OrsReportSummary
    {
        public void CreateExcel(Int32 allotmentID,String fundsource,DateTime dateFrom,DateTime dateTo)
        {
            BudgetDB db = new BudgetDB();
            FileInfo excelFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/ORSSUMMARY.xlsx"));
            try
            {
                FileInfo tempExcel = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/ORSSUMMARY2.xlsx"));
                tempExcel.Delete();
                excelFile.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/ORSSUMMARY2.xlsx"));
            }
            catch
            {
                excelFile.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/ORSSUMMARY2.xlsx"));
            }

            FileInfo newFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/ORSSUMMARY2.xlsx"));

            ExcelPackage pck = new ExcelPackage(newFile);
            ExcelWorksheet worksheet = pck.Workbook.Worksheets[1];

            Int32 startRow = 2;
            var orsList = db.ors.Where(p => p.allotment == allotmentID && p.FundSource == fundsource && p.deleted == false && p.Date >= dateFrom && p.Date <= dateTo).ToList();

            foreach (ORS ors in orsList)
            {
                worksheet.Cells[startRow, 1].Value = ors.Date.ToShortDateString();
                worksheet.Cells[startRow, 2].Value = ors.DB;
                worksheet.Cells[startRow, 3].Value = ors.PAYEE;
                worksheet.Cells[startRow, 4].Value = ors.Row;
                worksheet.Cells[startRow, 5].Value = ors.FundSource;
                startRow++;
            }
            pck.Save();
        }
    }
}