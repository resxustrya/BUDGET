namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubAllotmentHeaders", "TitleCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubAllotmentHeaders", "TitleCode");
        }
    }
}
