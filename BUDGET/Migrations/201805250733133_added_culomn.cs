namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_culomn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORSMasters", "allotments", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORSMasters", "allotments");
        }
    }
}
