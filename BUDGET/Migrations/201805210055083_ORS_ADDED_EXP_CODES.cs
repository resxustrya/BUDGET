namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ORS_ADDED_EXP_CODES : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORS", "EXP_CODE_10", c => c.String());
            AddColumn("dbo.ORS", "Amount_10", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORS", "Amount_10");
            DropColumn("dbo.ORS", "EXP_CODE_10");
        }
    }
}
