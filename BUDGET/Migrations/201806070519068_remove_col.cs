namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_col : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SubAllotmentHeaders", "fundsource");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubAllotmentHeaders", "fundsource", c => c.String());
        }
    }
}
