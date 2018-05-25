namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_models : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ORSCOes");
            DropTable("dbo.ORSMOOEs");
            DropTable("dbo.ORSPS");
            DropTable("dbo.ORSVTFs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ORSVTFs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        Date = c.String(),
                        DB = c.String(),
                        PO = c.String(),
                        PR = c.String(),
                        PAYEE = c.String(),
                        Adress = c.String(),
                        Particulars = c.String(),
                        ORS_NO = c.String(),
                        FundSource = c.String(),
                        Gross = c.String(),
                        EXP_CODE_1 = c.String(),
                        Amount_1 = c.Double(nullable: false),
                        EXP_CODE_2 = c.String(),
                        Amount_2 = c.Double(nullable: false),
                        EXP_CODE_3 = c.String(),
                        Amount_3 = c.Double(nullable: false),
                        EXP_CODE_4 = c.String(),
                        Amount_4 = c.Double(nullable: false),
                        EXP_CODE_5 = c.String(),
                        Amount_5 = c.Double(nullable: false),
                        EXP_CODE_6 = c.String(),
                        Amount_6 = c.Double(nullable: false),
                        EXP_CODE_7 = c.String(),
                        Amount_7 = c.Double(nullable: false),
                        EXP_CODE_8 = c.String(),
                        Amount_8 = c.Double(nullable: false),
                        EXP_CODE_9 = c.String(),
                        Amount_9 = c.Double(nullable: false),
                        AE = c.String(),
                        AF = c.String(),
                        AG = c.String(),
                        AH = c.String(),
                        AI = c.String(),
                        Created_By = c.String(),
                        Year = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ORSPS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        Date = c.String(),
                        DateReceived = c.String(),
                        TimeReceived = c.String(),
                        DateReleased = c.String(),
                        TimeReleased = c.String(),
                        DB = c.String(),
                        PO = c.String(),
                        PR = c.String(),
                        PAYEE = c.String(),
                        Adress = c.String(),
                        Particulars = c.String(),
                        ORS_NO = c.String(),
                        FundSource = c.String(),
                        Gross = c.String(),
                        EXP_CODE_1 = c.String(),
                        Amount_1 = c.Double(nullable: false),
                        EXP_CODE_2 = c.String(),
                        Amount_2 = c.Double(nullable: false),
                        EXP_CODE_3 = c.String(),
                        Amount_3 = c.Double(nullable: false),
                        EXP_CODE_4 = c.String(),
                        Amount_4 = c.Double(nullable: false),
                        EXP_CODE_5 = c.String(),
                        Amount_5 = c.Double(nullable: false),
                        EXP_CODE_6 = c.String(),
                        Amount_6 = c.Double(nullable: false),
                        EXP_CODE_7 = c.String(),
                        Amount_7 = c.Double(nullable: false),
                        EXP_CODE_8 = c.String(),
                        Amount_8 = c.Double(nullable: false),
                        EXP_CODE_9 = c.String(),
                        Amount_9 = c.Double(nullable: false),
                        AE = c.String(),
                        AF = c.String(),
                        AG = c.String(),
                        AH = c.String(),
                        AI = c.String(),
                        Created_By = c.String(),
                        Year = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ORSMOOEs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        Date = c.String(),
                        DateReceived = c.String(),
                        TimeReceived = c.String(),
                        DateReleased = c.String(),
                        TimeReleased = c.String(),
                        DB = c.String(),
                        PO = c.String(),
                        PR = c.String(),
                        PAYEE = c.String(),
                        Adress = c.String(),
                        Particulars = c.String(),
                        ORS_NO = c.String(),
                        FundSource = c.String(),
                        Gross = c.String(),
                        EXP_CODE_1 = c.String(),
                        Amount_1 = c.Double(nullable: false),
                        EXP_CODE_2 = c.String(),
                        Amount_2 = c.Double(nullable: false),
                        EXP_CODE_3 = c.String(),
                        Amount_3 = c.Double(nullable: false),
                        EXP_CODE_4 = c.String(),
                        Amount_4 = c.Double(nullable: false),
                        EXP_CODE_5 = c.String(),
                        Amount_5 = c.Double(nullable: false),
                        EXP_CODE_6 = c.String(),
                        Amount_6 = c.Double(nullable: false),
                        EXP_CODE_7 = c.String(),
                        Amount_7 = c.Double(nullable: false),
                        EXP_CODE_8 = c.String(),
                        Amount_8 = c.Double(nullable: false),
                        EXP_CODE_9 = c.String(),
                        Amount_9 = c.Double(nullable: false),
                        AE = c.String(),
                        AF = c.String(),
                        AG = c.String(),
                        AH = c.String(),
                        AI = c.String(),
                        Created_By = c.String(),
                        Year = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ORSCOes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        Date = c.String(),
                        DB = c.String(),
                        PO = c.String(),
                        PR = c.String(),
                        PAYEE = c.String(),
                        Adress = c.String(),
                        Particulars = c.String(),
                        ORS_NO = c.String(),
                        FundSource = c.String(),
                        Gross = c.String(),
                        EXP_CODE_1 = c.String(),
                        Amount_1 = c.Double(nullable: false),
                        EXP_CODE_2 = c.String(),
                        Amount_2 = c.Double(nullable: false),
                        EXP_CODE_3 = c.String(),
                        Amount_3 = c.Double(nullable: false),
                        EXP_CODE_4 = c.String(),
                        Amount_4 = c.Double(nullable: false),
                        EXP_CODE_5 = c.String(),
                        Amount_5 = c.Double(nullable: false),
                        EXP_CODE_6 = c.String(),
                        Amount_6 = c.Double(nullable: false),
                        EXP_CODE_7 = c.String(),
                        Amount_7 = c.Double(nullable: false),
                        EXP_CODE_8 = c.String(),
                        Amount_8 = c.Double(nullable: false),
                        EXP_CODE_9 = c.String(),
                        Amount_9 = c.Double(nullable: false),
                        AE = c.String(),
                        AF = c.String(),
                        AG = c.String(),
                        AH = c.String(),
                        AI = c.String(),
                        Created_By = c.String(),
                        Year = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
