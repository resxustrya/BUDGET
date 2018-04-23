using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BUDGET
{
    public static class GlobalYear
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
    }
}