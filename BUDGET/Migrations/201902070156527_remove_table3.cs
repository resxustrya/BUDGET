namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_table3 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UACS_SUB_CODES");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UACS_SUB_CODES",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        headerID = c.Int(nullable: false),
                        ExpenseTitle = c.String(),
                        ExpenseCode = c.String(),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
        }
    }
}
