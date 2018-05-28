namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_code_remove_mooe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allotments", "Code", c => c.String());
            DropTable("dbo.MOOEs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MOOEs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Line = c.Int(nullable: false),
                        Paraticulars = c.String(nullable: false),
                        UACS = c.String(nullable: false),
                        STO_Operations = c.Double(nullable: false),
                        PHM = c.Double(nullable: false),
                        RRHFS = c.Double(nullable: false),
                        HSRD = c.Double(nullable: false),
                        LHSDA = c.Double(nullable: false),
                        HRHICM = c.Double(nullable: false),
                        HRDP = c.Double(nullable: false),
                        HP = c.Double(nullable: false),
                        ES = c.Double(nullable: false),
                        HEPR = c.Double(nullable: false),
                        Year = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Allotments", "Code");
        }
    }
}
