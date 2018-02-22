namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MOOEs");
            AddColumn("dbo.MOOEs", "Line", c => c.Int(nullable: false));
            AlterColumn("dbo.MOOEs", "Paraticulars", c => c.String(nullable: false));
            AlterColumn("dbo.PersonnelServices", "Particulars", c => c.String(nullable: false));
            AlterColumn("dbo.PersonnelServices", "UACS", c => c.String(nullable: false));
            AddPrimaryKey("dbo.MOOEs", new[] { "ID", "Line" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MOOEs");
            AlterColumn("dbo.PersonnelServices", "UACS", c => c.String());
            AlterColumn("dbo.PersonnelServices", "Particulars", c => c.String());
            AlterColumn("dbo.MOOEs", "Paraticulars", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.MOOEs", "Line");
            AddPrimaryKey("dbo.MOOEs", new[] { "ID", "Paraticulars" });
        }
    }
}
