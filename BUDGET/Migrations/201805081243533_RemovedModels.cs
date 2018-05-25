namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedModels : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.BudgetSourceFunds");
            DropTable("dbo.FundSources");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FundSources",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SourceTitle = c.String(),
                        prexc = c.String(),
                        uacs = c.String(),
                        Year = c.String(),
                        ABR = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BudgetSourceFunds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        prexc = c.String(),
                        Line = c.Int(nullable: false),
                        allotment = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
