using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace BUDGET
{
    public class SaobExcel
    {
        public void CreateExcel(String date_from, String date_to)
        {
            BudgetDB db = new BudgetDB();
            FileInfo excelFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB_NEW.xlsx"));

            
            try
            {
                FileInfo tempExcel = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB_NEW2.xlsx"));
                tempExcel.Delete();
                excelFile.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB_NEW2.xlsx"));
            }
            catch
            {
                excelFile.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB_NEW2.xlsx"));
            }
            
            FileInfo newFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB_NEW2.xlsx"));

            new SaobExcelSheet1().CreateExcel(db,newFile,date_from,date_to);
            new SaobExcelSheet2().CreateExcel(db, newFile, date_from, date_to);
            


        }
    }
}