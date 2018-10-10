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
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("WorkSheet1");

                var Worksheet = excel.Workbook.Worksheets["Worksheet1"];
                
                // ROW 1 HEADERS

                Worksheet.Cells[1,1].Value = "P/A/P/ ALLOTMENT CLASS/";
                Worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(1).Width = 50;


                Worksheet.Cells[1, 2].Value = "EXPENSES";
                Worksheet.Cells[1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(2).Width = 15;

                Worksheet.Cells[1, 3].Value = "ALLOTMENT";
                Worksheet.Cells[1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(3).Width = 25;

                Worksheet.Cells[1, 4].Value = "REALIGNMENTS";
                Worksheet.Cells[1, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(4).Width = 50;

                Worksheet.Cells[1, 5].Value = "OBLIGATIONS INCURRED";
                Worksheet.Cells[1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(5).Width = 50;

                Worksheet.Cells[1, 5].Value = "UNOBLIGATED";
                Worksheet.Cells[1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(5).Width = 50;


                Worksheet.Cells[1, 6].Value = "DISBURSEMENTS";
                Worksheet.Cells[1, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(6).Width = 25;


                // ROW 2 HEADERS


                Worksheet.Cells[2, 1].Value = "OBJECT OF EXPENDITURE";
                Worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(1).Width = 50;

                Worksheet.Cells[2, 2].Value = "CODE";
                Worksheet.Cells[2, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(2).Width = 15;


                Worksheet.Cells[2, 3].Value = "RECEIVED";
                Worksheet.Cells[2, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(3).Width = 25;

                Worksheet.Cells[2, 3].Value = "REALIGNMENT";
                Worksheet.Cells[2, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(3).Width = 10;

                Worksheet.Cells[2, 3].Value = "TRANSFER TO";
                Worksheet.Cells[2, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                Worksheet.Column(4).Width = 10;


                Worksheet.Cells[2, 4].Value = "TOTAL AFTER REALIGNMENT";
                Worksheet.Cells[2, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                


                FileInfo excelFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/saob.xlsx"));
                excel.SaveAs(excelFile);
            }
        }
    }
}