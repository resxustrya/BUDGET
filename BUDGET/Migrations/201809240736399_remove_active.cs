namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_active : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FundSourceAmounts", "active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FundSourceAmounts", "active", c => c.Int(nullable: false));
        }
    }
}
