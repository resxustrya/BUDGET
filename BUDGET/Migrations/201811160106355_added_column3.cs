namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_column3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UACS_SubAllotment", "Description", c => c.String());
            DropTable("dbo.ExpenseAllotmentExpenseCodes");
            DropTable("dbo.ExpenseCodeAllotments");
        }
        
        public override void Down()
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
            
            DropColumn("dbo.UACS_SubAllotment", "Description");
        }
    }
}
