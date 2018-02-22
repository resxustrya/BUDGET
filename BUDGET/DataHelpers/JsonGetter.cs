using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using BUDGET.Models;

namespace BUDGET.DataHelpers
{
    public class JsonGetter
    {
        private BudgetDB db = new BudgetDB();
        
        public int GetLastLine(String table)
        {
            int LastLine = 0;
            switch(table)
            {
                case "PS":
                    LastLine = db.ps.LastOrDefault().Line;
                    
                    break;
                case "MOOE":
                    LastLine = db.mooe.LastOrDefault().Line;
                    break;
            }
            if (LastLine == 0)
                LastLine = 0;
            return LastLine;
        }
    }
}