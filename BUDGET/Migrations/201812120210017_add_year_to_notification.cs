namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_year_to_notification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Year", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Year");
        }
    }
}
