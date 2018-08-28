namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expenseallotmentexpensecodes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseAllotmentExpenseCodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExpenseCodeAllotment = c.Int(nullable: false),
                        uacs = c.String(),
                        amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        

        public override void Down()
        {
            DropTable("dbo.ExpenseAllotmentExpenseCodes");
        }
    }
}
