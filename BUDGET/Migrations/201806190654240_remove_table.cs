namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundSourceHdrs", "type", c => c.String());
            DropTable("dbo.SubAllotmentAmounts");
            DropTable("dbo.SubAllotmentHeaders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubAllotmentHeaders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        allotment = c.String(),
                        allotment_for = c.String(),
                        TitleCode = c.String(),
                        prexc = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SubAllotmentAmounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        expensecode = c.String(),
                        amount = c.Double(nullable: false),
                        saahdr = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.FundSourceHdrs", "type");
        }
    }
}
