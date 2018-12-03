namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_table2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ExcelFilenames");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExcelFilenames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Filename = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
