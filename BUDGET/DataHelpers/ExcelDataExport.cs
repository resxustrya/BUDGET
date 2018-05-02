using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BUDGET.Models;
using BUDGET.DataHelpers;
using System.Globalization;
using System.IO;
namespace BUDGET.DataHelpers
{
    public class ExcelDataExport
    {
        private BudgetDB db = new BudgetDB();
        private JsonGetter jg = new JsonGetter();

        public byte[] MOOE()
        {
            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                List<MOOEs> mooelist = jg.GetMOOE();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("MOOE");
                int startRowFrom = 2;

                DataTable table = new DataTable();
                table.Columns.Add("PARTICULARS");
                table.Columns.Add("UACS");
                table.Columns.Add("STO-Operations of Regional Offices");
                table.Columns.Add("Public Health Management");
                table.Columns.Add("Regulation of Regional Health Facilities and Services");
                table.Columns.Add("Health Sector Research Development");
                table.Columns.Add("Local Health Systems Development and Assistance");
                table.Columns.Add("Human Resources for Health (HRH) and Institutional Capacity Management");
                table.Columns.Add(" Human Resource for Health Deployment");
                table.Columns.Add("Health Promotion");
                table.Columns.Add("Epidemiology and Surveillance");
                table.Columns.Add("Health Emergency Preparedness and Response");
                
                    
                foreach(MOOEs mooe in mooelist)
                {
                    String sto_operation = mooe.STO_Operations > 0 ? mooe.STO_Operations.ToString("N", new CultureInfo("en-US")) : "";
                    String phm = mooe.PHM > 0 ? mooe.PHM.ToString("N", new CultureInfo("en-US")) : "";
                    String rrhfs = mooe.RRHFS > 0 ? mooe.RRHFS.ToString("N", new CultureInfo("en-US")) : "";
                    String hsrd = mooe.HSRD > 0 ?mooe.HSRD.ToString("N", new CultureInfo("en-US")) : "";
                    String lhsda = mooe.LHSDA > 0 ? mooe.LHSDA.ToString("N", new CultureInfo("en-US")) : "";
                    String hrhicm = mooe.HRHICM > 0 ? mooe.HRHICM.ToString("N", new CultureInfo("en-US")) : "";
                    String hrdp = mooe.HRDP > 0 ? mooe.HRDP.ToString("N", new CultureInfo("en-US")) : "";
                    String hp = mooe.HP > 0 ? mooe.HP.ToString("N", new CultureInfo("en-US")) : "";
                    String es = mooe.ES > 0 ? mooe.ES.ToString("N", new CultureInfo("en-US")) : "";
                    String hepr = mooe.HEPR > 0 ? mooe.HEPR.ToString("N", new CultureInfo("en-US")) : "";
                    table.Rows.Add(mooe.Paraticulars,mooe.UACS,sto_operation, phm, rrhfs, hsrd, lhsda, hrhicm, hrdp,hp, es, hepr);
                }
                
                worksheet.Cells["A" + startRowFrom].LoadFromDataTable(table, true);
                int columnIndex = 1;
                foreach(DataColumn column in table.Columns)
                {
                    ExcelRange columnCells = worksheet.Cells[worksheet.Dimension.Start.Row, columnIndex, worksheet.Dimension.End.Row, columnIndex];
                    
                    worksheet.Column(columnIndex).AutoFit();
                    
                    columnIndex++;
                }
                using (ExcelRange r = worksheet.Cells[startRowFrom, 1, startRowFrom, table.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Font.Name = "Times New Roman";
                    r.Style.Font.Size = 12;
                    r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.WrapText = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }

                using (ExcelRange r = worksheet.Cells[startRowFrom + 1, 1, startRowFrom + table.Rows.Count, table.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                   // r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Font.Name = "Times New Roman";
                    r.Style.Numberformat.Format = "#,##0.00";

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }
                
                worksheet.Cells["A1"].Value = "MOOE";
                worksheet.Cells["A1"].Style.Font.Size = 20;

                worksheet.InsertColumn(1, 1);
                worksheet.InsertRow(1, 1);
                worksheet.Column(1).Width = 5;
                result = package.GetAsByteArray();
            }
            return result;
        }

        public byte[] ReadReports()
        {
            byte[] result = null;
            FileInfo fileinfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/gaa.xlsx"));
            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = p.Workbook.Worksheets[3];
                    ws.Cells[1, 1].Value = "HAHAHEHE";
                    p.Save();
                    result = p.GetAsByteArray();
                }
            }
            return result;
        }

        public byte[] SAOBREPORT()
        {
            byte[] result = null;
            FileInfo fileinfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/2018_RO7_DOH_SAOB.xlsx"));
            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    result = p.GetAsByteArray();
                }
            }
            return result;
        }
    }
}