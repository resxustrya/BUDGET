namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_column5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "status");
        }
    }
}
