namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_col : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS", "head_requesting_office", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORS", "head_requesting_office");
        }
    }
}
