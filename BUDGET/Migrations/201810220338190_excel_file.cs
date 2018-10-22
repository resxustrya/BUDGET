namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class excel_file : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.ExcelFilenames");
        }
    }
}
