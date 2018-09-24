namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class active2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundSourceAmounts", "active", c => c.Int(nullable: false));
            AddColumn("dbo.FundSourceHdrs", "active", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FundSourceHdrs", "active");
            DropColumn("dbo.FundSourceAmounts", "active");
        }
    }
}
