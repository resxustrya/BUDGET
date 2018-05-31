namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_culomn1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubAllotmentHeaders", "allotment_for", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubAllotmentHeaders", "allotment_for");
        }
    }
}
