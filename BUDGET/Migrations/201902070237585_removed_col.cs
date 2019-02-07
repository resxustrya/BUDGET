namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removed_col : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrsDateEntries", "StringDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrsDateEntries", "StringDate", c => c.String());
        }
    }
}
