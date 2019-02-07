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
    public static class ORSHelper
    {
        public static String GetOrsCode(String ors_allotment)
        {
            BudgetDB db = new BudgetDB();
            var allotment = db.allotments.Where(p => p.ID.ToString() == ors_allotment).FirstOrDefault();
            return allotment.Code2 ?? "";
        }
        public static void InsertORSNo(Int32 id)
        {
            BudgetDB db = new BudgetDB();
            try
            {
                var this_ors = db.ors.Where(p => p.ID == id).FirstOrDefault();
                if(this_ors.Row == 0)
                {
                    var last_ors = db.ors.Where(p => p.allotment == this_ors.allotment && p.ID != this_ors.ID && p.Row != 0).OrderByDescending(p => p.Row).FirstOrDefault();
                    if (last_ors == null)
                        this_ors.Row = 1;
                    else
                    {
                        Int32 lastRow = last_ors.Row;
                        this_ors.Row = ++lastRow;
                    }
                    db.SaveChanges();
                }
            }
            catch { }
        }
        public static String FormatDate(DateTime date)
        {
            if (date != null)
                return date.ToShortDateString();
            return "";
        }
    }
}