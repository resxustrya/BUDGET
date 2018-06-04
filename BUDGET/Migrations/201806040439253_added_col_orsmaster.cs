namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_col_orsmaster : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORSMasters", "TitlCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ORSMasters", "TitlCode");
        }
    }
}
