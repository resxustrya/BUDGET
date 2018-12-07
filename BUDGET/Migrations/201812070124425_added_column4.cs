namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_column4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrsDateEntries", "StringDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrsDateEntries", "StringDate");
        }
    }
}
