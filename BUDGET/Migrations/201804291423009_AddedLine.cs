namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BudgetSourceFunds", "Line", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BudgetSourceFunds", "Line");
        }
    }
}
