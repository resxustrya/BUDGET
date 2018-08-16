namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ors_head_in_fsh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundSourceHdrs", "ors_head", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FundSourceHdrs", "ors_head");
        }
    }
}
