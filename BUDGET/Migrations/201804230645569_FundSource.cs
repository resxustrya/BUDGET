namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FundSource : DbMigration
    {
        public override void Up()
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
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FundSources");
        }
    }
}
