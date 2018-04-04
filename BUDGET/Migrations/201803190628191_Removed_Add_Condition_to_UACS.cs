namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_Add_Condition_to_UACS : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UACS", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.UACS", "Code", c => c.String(nullable: false));
            DropColumn("dbo.UACS", "Level");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UACS", "Level", c => c.Int(nullable: false));
            AlterColumn("dbo.UACS", "Code", c => c.String());
            AlterColumn("dbo.UACS", "Title", c => c.String());
        }
    }
}
