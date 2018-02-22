namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Personnel : DbMigration
    {
        public override void Up()
        {
           
            CreateTable(
               "dbo.PersonnelServices",
               c => new
               {
                   ID = c.Int(nullable: false, identity: true),
                   Particulars = c.String(),
                   UACS = c.String(),
                   STO_Operations = c.Double(),
                   PHM = c.Double(),
                   RRHFS = c.Double()
               }).PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("dbo.PersonnelServices");
        }
    }
}
