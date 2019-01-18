namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_col : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allotments", "previous", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allotments", "previous");
        }
    }
}
