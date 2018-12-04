namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_new_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrsDateEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExpenseTitle = c.String(),
                        ExpenseCode = c.String(),
                        ors_id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        amount = c.Double(nullable: false),
                        NetAmount = c.Double(nullable: false),
                        TaxAmount = c.Double(nullable: false),
                        Others = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrsDateEntries");
        }
    }
}
