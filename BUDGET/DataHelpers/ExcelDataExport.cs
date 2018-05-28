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