namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS_EXPENSE_CODES", "NetAmount", c => c.Double(nullable: false));
            AddColumn("dbo.ORS_EXPENSE_CODES", "TaxAmount", c => c.Double(nullable: false));
            DropColumn("dbo.ORS_EXPENSE_CODES", "Disboursement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ORS_EXPENSE_CODES", "Disboursement", c => c.Double(nullable: false));
            DropColumn("dbo.ORS_EXPENSE_CODES", "TaxAmount");
            DropColumn("dbo.ORS_EXPENSE_CODES", "NetAmount");
        }
    }
}
