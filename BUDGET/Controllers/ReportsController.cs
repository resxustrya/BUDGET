using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BUDGET.DataHelpers;
namespace BUDGET.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        // GET: Reports
        public FileContentResult SAOB()
        {
            ExcelDataExport report = new ExcelDataExport();
            string contenttype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            byte[] filecontent = report.SAOBREPORT();
            return File(filecontent, contenttype, "SAOB.xlsx");
        }
    }
}