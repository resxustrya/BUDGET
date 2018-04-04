namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ORSPS", "Date", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ORSPS", "Date", c => c.DateTime(nullable: false));
        }
    }
}
