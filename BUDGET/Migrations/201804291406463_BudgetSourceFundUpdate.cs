namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetSourceFundUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BudgetSourceFunds",
                c => new
                    {
                        prexc = c.String(nullable: false, maxLength: 128),
                        allotment = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.prexc);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BudgetSourceFunds");
        }
    }
}
