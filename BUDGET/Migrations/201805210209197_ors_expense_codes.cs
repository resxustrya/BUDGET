namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ors_expense_codes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ORS_EXPENSE_CODES",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ors_obligation = c.Int(nullable: false),
                        uacs = c.String(),
                        amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ORS_EXPENSE_CODES");
        }
    }
}
