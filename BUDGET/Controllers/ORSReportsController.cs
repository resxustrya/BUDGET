using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using BUDGET.DataHelpers;
namespace BUDGET.Controllers
{
    public class ORSReportsController : Controller
    {
        ORSReporting rpt_ors = new ORSReporting();
        // GET: ORSReports
        [Route("print/ors/ps/{id}",Name = "print_ors_ps")]
        public String  PrintOrsPS(String id)
        {
            return id;
        }
        [Route("print/ors/mooe/{id}",Name ="print_ors_mooe")]
        public ActionResult PrintORSMOOE(String id)
        {
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearContent();
            rpt_ors.GenerateORSMOOE(id);
            String filename = id + ".pdf";
            var response_file = new FileStream(Server.MapPath("/rpt_ors/mooe/" + filename), FileMode.Open);
            return File(response_file, "application/pdf", "MOOE.pdf");
        }
        
    }
}