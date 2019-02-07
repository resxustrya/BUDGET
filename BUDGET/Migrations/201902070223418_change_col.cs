namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_col : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrsDateEntries", "chequeNo", c => c.String());
            DropColumn("dbo.OrsDateEntries", "chequeNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrsDateEntries", "chequeNumber", c => c.String());
            DropColumn("dbo.OrsDateEntries", "chequeNo");
        }
    }
}
