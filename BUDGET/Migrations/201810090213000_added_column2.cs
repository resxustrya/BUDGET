namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_column2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS_EXPENSE_CODES", "Checklist", c => c.String());
            AddColumn("dbo.ORS_EXPENSE_CODES", "DateDisburset", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORS_EXPENSE_CODES", "DateDisburset");
            DropColumn("dbo.ORS_EXPENSE_CODES", "Checklist");
        }
    }
}
