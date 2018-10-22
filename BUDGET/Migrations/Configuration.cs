namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BudgetDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BudgetDB context)
        {
            var db = new BudgetDB();
            var excelfilenames = db.excelfilename.ToList();
            if(excelfilenames.Count <= 0)
            {
                var excel = new ExcelFilename();
                excel.Filename = "SAOB1.xlsx";
                db.excelfilename.Add(excel);
                db.SaveChanges();

            }
        }
    }
}
