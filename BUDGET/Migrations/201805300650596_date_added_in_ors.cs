namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date_added_in_ors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS", "Date_Added", c => c.DateTime(nullable: false));
            AddColumn("dbo.ORS", "dateadded", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORS", "dateadded");
            DropColumn("dbo.ORS", "Date_Added");
        }
    }
}
