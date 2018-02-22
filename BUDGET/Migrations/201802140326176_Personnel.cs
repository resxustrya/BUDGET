namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.SqlTypes;
    using System.Security.Principal;

    public partial class Personnel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonnelServices",
                c => new
                    {
                        ID = c.Int(nullable: false,identity:true),
                        Particulars = c.String(nullable:true),
                        UACS = c.String(nullable:true),
                        STO_Operations = c.Double(nullable: true),
                        PHM = c.Double(nullable: true),
                        RRHFS = c.Double(nullable: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.PersonnelServices");
        }
    }
}
