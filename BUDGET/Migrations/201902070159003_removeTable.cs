namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UACS_SubAllotment");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UACS_SubAllotment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FromExpenseTitle = c.String(),
                        fundsource = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
