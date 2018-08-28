namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expensecodeallotment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseCodeAllotments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fundsource = c.Int(nullable: false),
                        from_uacs = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExpenseCodeAllotments");
        }
    }
}
