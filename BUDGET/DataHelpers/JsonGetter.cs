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
            try
            {
                switch (table)
                {
                    case "PS":
                        int psline = db.ps.OrderByDescending(x => x.Line).FirstOrDefault().Line;
                        LastLine = psline;
                        break;
                    case "MOOE":
                        int mooline = db.mooe.OrderByDescending(x => x.Line).FirstOrDefault().Line;
                        LastLine = mooline;
                        break;
                    
                }
            }catch(Exception es)
            {

            }
            if (LastLine == 0)
                return LastLine = 1;
            return LastLine + 1;
        }
        public List<MOOEs> GetMOOE()
        {
            var query = from list in db.mooe
                        orderby list.Line ascending
                        select list;
            List<MOOEs> ps = query.ToList<MOOEs>();
            return ps;
        }

    }
}