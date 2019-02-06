namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_col1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS", "deleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.ORS", "AE");
            DropColumn("dbo.ORS", "AF");
            DropColumn("dbo.ORS", "AG");
            DropColumn("dbo.ORS", "AH");
            DropColumn("dbo.ORS", "AI");
            DropColumn("dbo.ORS", "head_requesting_office");
            DropColumn("dbo.ORS", "is_obligated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ORS", "is_obligated", c => c.String());
            AddColumn("dbo.ORS", "head_requesting_office", c => c.String());
            AddColumn("dbo.ORS", "AI", c => c.String());
            AddColumn("dbo.ORS", "AH", c => c.String());
            AddColumn("dbo.ORS", "AG", c => c.String());
            AddColumn("dbo.ORS", "AF", c => c.String());
            AddColumn("dbo.ORS", "AE", c => c.String());
            DropColumn("dbo.ORS", "deleted");
        }
    }
}
