namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_col1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrsDateEntries", "Date", c => c.DateTime());
            AlterColumn("dbo.OrsDateEntries", "chequeDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrsDateEntries", "chequeDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrsDateEntries", "Date", c => c.DateTime(nullable: false));
        }
    }
}
