namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expense_title : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundSourceAmounts", "expense_title", c => c.String());
            AddColumn("dbo.FundSourceAmounts", "expense_code", c => c.String());
            DropColumn("dbo.FundSourceAmounts", "expensecode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FundSourceAmounts", "expensecode", c => c.String());
            DropColumn("dbo.FundSourceAmounts", "expense_code");
            DropColumn("dbo.FundSourceAmounts", "expense_title");
        }
    }
}
