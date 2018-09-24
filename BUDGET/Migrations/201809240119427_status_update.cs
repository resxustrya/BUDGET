namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YearBudgets", "active", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.YearBudgets", "active");
        }
    }
}
