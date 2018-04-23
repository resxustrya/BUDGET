using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUDGET.Filters;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using BUDGET.Models;
namespace BUDGET.Controllers
{
    [Authorize]
    [YearlyFilter]
    public class FundSourceController : Controller
    {
        BudgetDB db = new BudgetDB();
        // GET: FundSource
        public ActionResult Index()
        {
            ViewBag.Menu = "Fund Source";
            return View();
        }
        [Route("get/fund-source",Name ="get_fund_source")]
        public JsonResult GetFundSource()
        {
            var fund_source = (from list in db.fund_source
                         orderby list.ID ascending
                         select new
                         {
                             ID = list.ID,
                             SourceTitle = list.SourceTitle,
                             prexc = list.prexc,
                             uacs = list.uacs,
                             ABR = list.ABR
                         }).ToList();
            return Json(fund_source, JsonRequestBehavior.AllowGet);
        }
        [Route("save/fund-sournce",Name = "save_fund_source")]
        public JsonResult SaveFundSource(String data)
        {
            List<Object> list = JsonConvert.DeserializeObject<List<Object>>(data);
            Int32 id = 0;
            foreach (Object s in list)
            {
                try
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    //var ps = db.ps.Where(p => p.ID == sb.ID).FirstOrDefault();
                    id = Convert.ToInt32(sb.ID);
                    var fund_source = db.fund_source.Where(p => p.ID == id).FirstOrDefault();
                    fund_source.SourceTitle = sb.SourceTitle;
                    fund_source.prexc = sb.prexc;
                    fund_source.uacs = sb.uacs;
                    fund_source.ABR = sb.ABR;

                    try { db.SaveChanges(); } catch { }
                }
                catch (Exception ex)
                {
                    dynamic sb = JsonConvert.DeserializeObject<dynamic>(s.ToString());
                    try
                    {
                        if (sb.SourceTitle != null)
                        {
                            FundSource fund_source = new FundSource();
                            fund_source.SourceTitle = sb.SourceTitle;
                            fund_source.prexc = sb.prexc;
                            fund_source.uacs = sb.uacs;
                            fund_source.Year = GlobalYear.Year;
                            fund_source.ABR = sb.ABR;
                            db.fund_source.Add(fund_source);
                            try { db.SaveChanges(); } catch { }
                        }

                    }
                    catch { }
                }

            }
            return GetFundSource();
        }
        [Route("delete/fund-source",Name = "delete_fund_source")]
        public String DeleteFundSource(String data)
        {
            return data;
        }
    }
}