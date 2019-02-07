namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_col2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrsDateEntries", "chequeNumber", c => c.String());
            AddColumn("dbo.OrsDateEntries", "chequeDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrsDateEntries", "chequeDate");
            DropColumn("dbo.OrsDateEntries", "chequeNumber");
        }
    }
}
