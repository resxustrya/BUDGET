namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_datetime_format : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ORS", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ORS", "Date", c => c.String());
        }
    }
}
