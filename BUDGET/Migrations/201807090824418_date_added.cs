namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS", "Date1", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORS", "Date1");
        }
    }
}
