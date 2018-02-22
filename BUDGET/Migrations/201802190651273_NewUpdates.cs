namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewUpdates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MOOEs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Paraticulars = c.String(),
                        UACS = c.String(),
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
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MOOEs");
        }
    }
}
