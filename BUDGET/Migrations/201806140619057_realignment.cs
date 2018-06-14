namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class realignment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Realignments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        uacs_from = c.String(),
                        uacs_to = c.String(),
                        amount = c.Double(nullable: false),
                        fundsource = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Realignments");
        }
    }
}
