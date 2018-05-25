namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FundSourceHdr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FundSourceHdrs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        prexc = c.String(),
                        SourceTitle = c.String(),
                        allotment = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FundSourceHdrs");
        }
    }
}
