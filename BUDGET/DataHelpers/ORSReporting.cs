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
        
        public String GetOrsCode(String ors_id)
        {
            var orscode = (from list in db.orsmaster join allotment in db.allotments on list.allotments equals allotment.ID where list.ID.ToString() == ors_id select allotment.Code2).FirstOrDefault();
            return orscode ?? "";
        }
    }
}