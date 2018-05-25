namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DESC_FUND_SOURCE_HDR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundSourceHdrs", "desc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FundSourceHdrs", "desc");
        }
    }
}
