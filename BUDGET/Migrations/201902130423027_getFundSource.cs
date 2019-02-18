namespace BUDGET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Text;

    public partial class getFundSource : DbMigration
    {
        public override void Up()
        {
            StringBuilder query = new StringBuilder();
            query.Append("CREATE PROCEDURE dbo.GetFundSource" + Environment.NewLine);
            query.Append("AS" + Environment.NewLine);
            query.Append("BEGIN" + Environment.NewLine);
            query.Append(@"SELECT 'Hello World'" + Environment.NewLine);
            query.Append("END" + Environment.NewLine);
            this.Sql(query.ToString());
        }
        public override void Down()
        {
            this.Sql("DROP PROCEDURE dbo.GetFundSource");
        }
    }
}
