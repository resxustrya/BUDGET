namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adde_table_and_column : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UACS_SubAllotment", "ExpenseTitle");
            DropColumn("dbo.UACS_SubAllotment", "ExpenseCode");
            DropColumn("dbo.UACS_SubAllotment", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UACS_SubAllotment", "Amount", c => c.Double(nullable: false));
            AddColumn("dbo.UACS_SubAllotment", "ExpenseCode", c => c.String());
            AddColumn("dbo.UACS_SubAllotment", "ExpenseTitle", c => c.String());
        }
    }
}
