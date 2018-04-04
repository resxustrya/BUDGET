namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UACS_LINE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UACS", "Line", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UACS", "Line");
        }
    }
}
