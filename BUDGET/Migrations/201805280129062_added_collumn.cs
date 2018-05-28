namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_collumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundSourceHdrs", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FundSourceHdrs", "Code");
        }
    }
}
