namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_column1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS", "is_obligated", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORS", "is_obligated");
        }
    }
}
