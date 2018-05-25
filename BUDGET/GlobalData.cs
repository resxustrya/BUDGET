using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BUDGET.Models;
namespace BUDGET
{
    public static class GlobalData
    {
        public static String Year
        {
            get
            {
                return HttpContext.Current.Application["Year"] as String;
            }
            set
            {
                HttpContext.Current.Application["Year"] = value;
            }
        }
        public static String BudgetID
        {
            get
            {
                return HttpContext.Current.Application["BUDGET_ID"] as String;
            }
            set
            {
                HttpContext.Current.Application["BUDGET_ID"] = value;
            }
        }

        public static String ors_id
        {
            get
            {
                return HttpContext.Current.Application["ors_id"] as String;
            }
            set
            {
                HttpContext.Current.Application["ors_id"] = value;
            }
        }
        public static String allotment
        {
            get
            {
                return HttpContext.Current.Application["allotment"] as String;
            }
            set
            {
                HttpContext.Current.Application["allotment"] = value;
            }
        }
    }
}