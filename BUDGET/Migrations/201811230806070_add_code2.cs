namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_code2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allotments", "Code2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allotments", "Code2");
        }
    }
}
