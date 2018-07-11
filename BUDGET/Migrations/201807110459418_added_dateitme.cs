namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_dateitme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "DateAdded");
        }
    }
}
