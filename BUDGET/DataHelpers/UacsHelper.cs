using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BUDGET
{
    public static class UacsHelper
    {
        public static Boolean ExpenseExist(String expense)
        {
            BudgetDB db = new BudgetDB();
            var ExpenseTitle = (from list in db.uacs where list.Title == expense select list).ToList();
            if(ExpenseTitle.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}