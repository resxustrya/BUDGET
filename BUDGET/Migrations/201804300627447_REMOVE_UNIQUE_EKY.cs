namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class REMOVE_UNIQUE_EKY : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BudgetSourceFunds");
            AddColumn("dbo.BudgetSourceFunds", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.BudgetSourceFunds", "prexc", c => c.String());
            AddPrimaryKey("dbo.BudgetSourceFunds", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.BudgetSourceFunds");
            AlterColumn("dbo.BudgetSourceFunds", "prexc", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.BudgetSourceFunds", "ID");
            AddPrimaryKey("dbo.BudgetSourceFunds", new[] { "prexc", "allotment" });
        }
    }
}
