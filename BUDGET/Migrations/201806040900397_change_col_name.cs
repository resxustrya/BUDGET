namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_col_name : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubAllotmentAmounts", "saahdr", c => c.String());
            DropColumn("dbo.SubAllotmentAmounts", "fundsource");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubAllotmentAmounts", "fundsource", c => c.String());
            DropColumn("dbo.SubAllotmentAmounts", "saahdr");
        }
    }
}
