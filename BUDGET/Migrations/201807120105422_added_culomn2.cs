namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_culomn2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Realignments", "type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Realignments", "type");
        }
    }
}
