namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_culomn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundSourceHdrs", "Responsibility_Number", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FundSourceHdrs", "Responsibility_Number");
        }
    }
}
