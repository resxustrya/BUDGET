using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using BUDGET.Models;
namespace BUDGET.DataHelpers
{
    public class ORSReporting
    {
        private BudgetDB db = new BudgetDB();
        
        public String GetOrsCode(String ors_allotment)
        {
            var allotment = db.allotments.Where(p => p.ID.ToString() == ors_allotment).FirstOrDefault();
            return allotment.Code2 ?? "";
        }
    }
}