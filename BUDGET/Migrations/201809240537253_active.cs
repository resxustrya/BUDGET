namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class active : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allotments", "active", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allotments", "active");
        }
    }
}
