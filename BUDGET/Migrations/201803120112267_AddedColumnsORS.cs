namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnsORS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORSPS", "AE", c => c.String());
            AddColumn("dbo.ORSPS", "AF", c => c.String());
            AddColumn("dbo.ORSPS", "AG", c => c.String());
            AddColumn("dbo.ORSPS", "AH", c => c.String());
            AddColumn("dbo.ORSPS", "AI", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORSPS", "AI");
            DropColumn("dbo.ORSPS", "AH");
            DropColumn("dbo.ORSPS", "AG");
            DropColumn("dbo.ORSPS", "AF");
            DropColumn("dbo.ORSPS", "AE");
        }
    }
}
