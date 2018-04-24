namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ORSMOOE_UPDATE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORSMOOEs", "DateReceived", c => c.String());
            AddColumn("dbo.ORSMOOEs", "TimeReceived", c => c.String());
            AddColumn("dbo.ORSMOOEs", "DateReleased", c => c.String());
            AddColumn("dbo.ORSMOOEs", "TimeReleased", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORSMOOEs", "TimeReleased");
            DropColumn("dbo.ORSMOOEs", "DateReleased");
            DropColumn("dbo.ORSMOOEs", "TimeReceived");
            DropColumn("dbo.ORSMOOEs", "DateReceived");
        }
    }
}
