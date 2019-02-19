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
            var orsList = db.ors.Where(p => p.allotment == allotmentID && p.FundSource == fundsource && p.deleted == false && p.Date >= dateFrom && p.Date <= dateTo).OrderBy(p => p.Date).ToList();


            foreach (ORS ors in orsList)
            {
                Int32 count = 1;
                Int32 uacsColStart = 7;
                Int32 uacsAmountColStart = 8;
                Double gross = 0;
                worksheet.Cells[startRow, 1].Value = ors.Date.ToShortDateString();
                worksheet.Cells[startRow, 2].Value = ors.DB;
                worksheet.Cells[startRow, 3].Value = ors.PAYEE;
                worksheet.Cells[startRow, 4].Value = ors.Row;
                worksheet.Cells[startRow, 5].Value = ors.FundSource;

                var orsUacs = (from _orsuacs in db.ors_expense_codes
                               join _uacs in db.uacs on _orsuacs.uacs equals _uacs.Title
                               where _orsuacs.ors_obligation == ors.ID
                               select new
                               {
                                   uacs = _uacs.Code,
                                   amount = _orsuacs.amount,
                               }).ToList();

                foreach(var oec in orsUacs)
                {
                    worksheet.Cells[startRow, uacsColStart].Value = Convert.ToInt64(oec.uacs);
                    worksheet.Cells[startRow, uacsColStart].Style.Numberformat.Format = "#";
                    worksheet.Cells[startRow, uacsColStart].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Column(uacsColStart).Width = 15;

                    worksheet.Cells[startRow, uacsAmountColStart].Style.Numberformat.Format = "#,##0.00";
                    worksheet.Cells[startRow, uacsAmountColStart].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[startRow, uacsAmountColStart].Value = oec.amount;
                    worksheet.Cells[startRow, uacsAmountColStart].AutoFitColumns();
                    worksheet.Column(uacsAmountColStart).Width = 15;

                    worksheet.Cells[1, uacsColStart].Value = "EXP CODE " + count;
                    worksheet.Cells[1, uacsAmountColStart].Value = "AMOUNT " + count;


                    gross += oec.amount;

                    uacsColStart += 2;
                    uacsAmountColStart += 2;
                    count++;

                }

                worksheet.Cells[startRow, 6].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 6].Value = gross;
                startRow++;
            }
            pck.Save();
        }
    }
}