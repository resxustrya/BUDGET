namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_cols : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ORS_EXPENSE_CODES", "Checklist");
            DropColumn("dbo.ORS_EXPENSE_CODES", "DateDisburset");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ORS_EXPENSE_CODES", "DateDisburset", c => c.DateTime(nullable: false));
            AddColumn("dbo.ORS_EXPENSE_CODES", "Checklist", c => c.String());
        }
    }
}
