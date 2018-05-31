namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class model_added_saahdr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubAllotmentHeaders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        allotment = c.String(),
                        prexc = c.String(),
                        fundsource = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SubAllotmentHeaders");
        }
    }
}
