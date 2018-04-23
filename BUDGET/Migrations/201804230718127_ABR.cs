namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ABR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundSources", "ABR", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FundSources", "ABR");
        }
    }
}
