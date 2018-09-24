namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class active1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORSMasters", "active", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORSMasters", "active");
        }
    }
}
