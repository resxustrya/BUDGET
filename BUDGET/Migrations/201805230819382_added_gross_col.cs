namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_gross_col : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS", "Gross", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORS", "Gross");
        }
    }
}
