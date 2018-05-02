namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allotments_changes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Allotments", "year", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Allotments", "year", c => c.Int(nullable: false));
        }
    }
}
