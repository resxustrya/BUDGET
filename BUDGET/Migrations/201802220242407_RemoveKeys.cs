namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveKeys : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MOOEs");
            DropPrimaryKey("dbo.PersonnelServices");
            AlterColumn("dbo.MOOEs", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.PersonnelServices", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MOOEs", "ID");
            AddPrimaryKey("dbo.PersonnelServices", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PersonnelServices");
            DropPrimaryKey("dbo.MOOEs");
            AlterColumn("dbo.PersonnelServices", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.MOOEs", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PersonnelServices", new[] { "ID", "Line" });
            AddPrimaryKey("dbo.MOOEs", new[] { "ID", "Line" });
        }
    }
}
