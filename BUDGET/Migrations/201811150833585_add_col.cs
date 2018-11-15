namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_col : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UACS_SubAllotment", "FromExpenseTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UACS_SubAllotment", "FromExpenseTitle");
        }
    }
}
