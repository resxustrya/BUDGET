namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_FundSourceAmount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FundSourceAmounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        expensecode = c.String(),
                        amount = c.Double(nullable: false),
                        fundsource = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FundSourceAmounts");
        }
    }
}
