namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MOOEs");
            DropPrimaryKey("dbo.PersonnelServices");
            AddColumn("dbo.PersonnelServices", "Line", c => c.Int(nullable: false));
            AlterColumn("dbo.MOOEs", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.MOOEs", "Paraticulars", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PersonnelServices", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MOOEs", new[] { "ID", "Paraticulars" });
            AddPrimaryKey("dbo.PersonnelServices", new[] { "ID", "Line" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PersonnelServices");
            DropPrimaryKey("dbo.MOOEs");
            AlterColumn("dbo.PersonnelServices", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.MOOEs", "Paraticulars", c => c.String(nullable: false));
            AlterColumn("dbo.MOOEs", "ID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.PersonnelServices", "Line");
            AddPrimaryKey("dbo.PersonnelServices", "ID");
            AddPrimaryKey("dbo.MOOEs", "ID");
        }
    }
}
