namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uacs_suballotment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UACS_SubAllotment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fundsource = c.Int(nullable: false),
                        ExpenseTitle = c.String(),
                        ExpenseCode = c.String(),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UACS_SubAllotment");
        }
    }
}
