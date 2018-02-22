namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MOOEs", "Paraticulars", c => c.String(nullable: false));
            AlterColumn("dbo.MOOEs", "UACS", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MOOEs", "UACS", c => c.String());
            AlterColumn("dbo.MOOEs", "Paraticulars", c => c.String());
        }
    }
}
