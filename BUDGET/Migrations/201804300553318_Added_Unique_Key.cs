namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Unique_Key : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BudgetSourceFunds");
            AddPrimaryKey("dbo.BudgetSourceFunds", new[] { "prexc", "allotment" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.BudgetSourceFunds");
            AddPrimaryKey("dbo.BudgetSourceFunds", "prexc");
        }
    }
}
