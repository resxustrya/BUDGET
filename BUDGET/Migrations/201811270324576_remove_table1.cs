namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_table1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS", "allotment", c => c.Int(nullable: false));
            DropColumn("dbo.ORS", "ors_id");
            DropTable("dbo.ORSMasters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ORSMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Year = c.String(),
                        allotments = c.Int(nullable: false),
                        TitlCode = c.String(),
                        active = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ORS", "ors_id", c => c.Int(nullable: false));
            DropColumn("dbo.ORS", "allotment");
        }
    }
}
