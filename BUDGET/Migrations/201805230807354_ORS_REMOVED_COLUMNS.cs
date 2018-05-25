namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ORS_REMOVED_COLUMNS : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ORS", "Gross");
            DropColumn("dbo.ORS", "EXP_CODE_1");
            DropColumn("dbo.ORS", "Amount_1");
            DropColumn("dbo.ORS", "EXP_CODE_2");
            DropColumn("dbo.ORS", "Amount_2");
            DropColumn("dbo.ORS", "EXP_CODE_3");
            DropColumn("dbo.ORS", "Amount_3");
            DropColumn("dbo.ORS", "EXP_CODE_4");
            DropColumn("dbo.ORS", "Amount_4");
            DropColumn("dbo.ORS", "EXP_CODE_5");
            DropColumn("dbo.ORS", "Amount_5");
            DropColumn("dbo.ORS", "EXP_CODE_6");
            DropColumn("dbo.ORS", "Amount_6");
            DropColumn("dbo.ORS", "EXP_CODE_7");
            DropColumn("dbo.ORS", "Amount_7");
            DropColumn("dbo.ORS", "EXP_CODE_8");
            DropColumn("dbo.ORS", "Amount_8");
            DropColumn("dbo.ORS", "EXP_CODE_9");
            DropColumn("dbo.ORS", "Amount_9");
            DropColumn("dbo.ORS", "EXP_CODE_10");
            DropColumn("dbo.ORS", "Amount_10");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ORS", "Amount_10", c => c.Double(nullable: false));
            AddColumn("dbo.ORS", "EXP_CODE_10", c => c.String());
            AddColumn("dbo.ORS", "Amount_9", c => c.Double(nullable: false));
            AddColumn("dbo.ORS", "EXP_CODE_9", c => c.String());
            AddColumn("dbo.ORS", "Amount_8", c => c.Double(nullable: false));
            AddColumn("dbo.ORS", "EXP_CODE_8", c => c.String());
            AddColumn("dbo.ORS", "Amount_7", c => c.Double(nullable: false));
            AddColumn("dbo.ORS", "EXP_CODE_7", c => c.String());
            AddColumn("dbo.ORS", "Amount_6", c => c.Double(nullable: false));
            AddColumn("dbo.ORS", "EXP_CODE_6", c => c.String());
            AddColumn("dbo.ORS", "Amount_5", c => c.Double(nullable: false));
            AddColumn("dbo.ORS", "EXP_CODE_5", c => c.String());
            AddColumn("dbo.ORS", "Amount_4", c => c.Double(nullable: false));
            AddColumn("dbo.ORS", "EXP_CODE_4", c => c.String());
            AddColumn("dbo.ORS", "Amount_3", c => c.Double(nullable: false));
            AddColumn("dbo.ORS", "EXP_CODE_3", c => c.String());
            AddColumn("dbo.ORS", "Amount_2", c => c.Double(nullable: false));
            AddColumn("dbo.ORS", "EXP_CODE_2", c => c.String());
            AddColumn("dbo.ORS", "Amount_1", c => c.Double(nullable: false));
            AddColumn("dbo.ORS", "EXP_CODE_1", c => c.String());
            AddColumn("dbo.ORS", "Gross", c => c.String());
        }
    }
}
