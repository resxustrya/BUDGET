namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_others : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS_EXPENSE_CODES", "Others", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORS_EXPENSE_CODES", "Others");
        }
    }
}
