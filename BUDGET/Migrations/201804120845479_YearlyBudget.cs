namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearlyBudget : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YearBudgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("dbo.YearBudgets");
        }
    }
}
