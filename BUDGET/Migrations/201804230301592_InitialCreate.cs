namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MOOEs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Line = c.Int(nullable: false),
                        Paraticulars = c.String(nullable: false),
                        UACS = c.String(nullable: false),
                        STO_Operations = c.Double(nullable: false),
                        PHM = c.Double(nullable: false),
                        RRHFS = c.Double(nullable: false),
                        HSRD = c.Double(nullable: false),
                        LHSDA = c.Double(nullable: false),
                        HRHICM = c.Double(nullable: false),
                        HRDP = c.Double(nullable: false),
                        HP = c.Double(nullable: false),
                        ES = c.Double(nullable: false),
                        HEPR = c.Double(nullable: false),
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
            
            CreateTable(
                "dbo.ORSMOOEs",
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
                "dbo.PREXCs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Line = c.Int(nullable: false),
                        Desc = c.String(),
                        Code1 = c.String(),
                        Code2 = c.String(),
                        Yearly = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PersonnelServices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Line = c.Int(nullable: false),
                        Particulars = c.String(nullable: false),
                        UACS = c.String(nullable: false),
                        STO_Operations = c.Double(nullable: false),
                        PHM = c.Double(nullable: false),
                        RRHFS = c.Double(nullable: false),
                        Year = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UACS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Line = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.YearBudgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.YearBudgets");
            DropTable("dbo.Users");
            DropTable("dbo.UACS");
            DropTable("dbo.PersonnelServices");
            DropTable("dbo.PREXCs");
            DropTable("dbo.ORSVTFs");
            DropTable("dbo.ORSPS");
            DropTable("dbo.ORSMOOEs");
            DropTable("dbo.ORSCOes");
            DropTable("dbo.MOOEs");
        }
    }
}
