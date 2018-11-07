using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BUDGET
{
    [Authorize(Roles ="Admin")]
    [YearlyFilter]
    [NoCache]
    [OutputCache(NoStore = true, Duration = 0)]
    public class RemovedDataController : Controller
    {
        BudgetDB db = new BudgetDB();
        // GET: RemovedData
        public ActionResult Index()
        {
            var allotments = db.allotments.Where(p => p.year == GlobalData.Year).ToList();
            return View(allotments);
        }

        [HttpPost]
        public void Allotment(FormCollection collection)
        {
            db.Database.ExecuteSqlCommand("UPDATE Allotments SET active = '" + collection.Get("status") + "' WHERE ID = '" + collection.Get("allotment") +"'");
            db.Database.ExecuteSqlCommand("UPDATE ORSMasters SET active = '" + collection.Get("status") + "' WHERE allotments ='" + collection.Get("allotment") + "'");
            db.Database.ExecuteSqlCommand("UPDATE FundSourceHdrs SET active = '"+ collection.Get("status")  +"' WHERE allotment ='" + collection.Get("allotment") + "'");
            db.SaveChanges();
        }

        public ActionResult FundSource(String ID)
        {
            var fsh = db.fsh.Where(p => p.allotment == ID && p.type == "REG").ToList();
            return View(fsh);
        }

        [HttpPost]
        public void FundSource(FormCollection collection)
        {
            db.Database.ExecuteSqlCommand("UPDATE FundSourceHdrs SET active = '" + collection.Get("status") + "' WHERE ID ='" + collection.Get("fundsource") + "'");
            db.SaveChanges();
        }
        
        public ActionResult DeleteFundSource(String ID)
        {
            var remove_fundsource = db.fsh.Where(p => p.ID.ToString() == ID).FirstOrDefault();
            var delete_uacs = db.fsa.Where(p => p.fundsource == remove_fundsource.ID.ToString()).ToList();

            db.fsh.Remove(remove_fundsource);
            db.fsa.RemoveRange(delete_uacs);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}