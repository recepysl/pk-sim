using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimApi.Data.Migrations
{
    public partial class InitialMigrationView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql("" +
                "CREATE VIEW dbo.\"vTransactionReport\"\r\n AS\r\n SELECT tr.\"Id\", \r\n   " +
                " tr.\"CreatedAt\",    \r\n    tr.\"CreatedBy\",    \r\n    tr.\"UpdatedAt\", " +
                "   \r\n    tr.\"UpdatedBy\",\r\n    tr.\"AccountId\",\r\n    tr.\"CurrencyCode\",\r\n    tr.\"Amount\",\r\n  " +
                "  tr.\"Direction\",\r\n    tr.\"TransactionDate\",\r\n    tr.\"Description\",\r\n " +
                "   tr.\"ReferenceNumber\",\r\n    tr.\"TransactionCode\",\r\n   " +
                " ac.\"Name\" AS \"AccountName\",\r\n    ac.\"AccountNumber\",\r\n  " +
                "  ac.\"CustomerId\",\r\n    cu.\"CustomerNumber\",\r\n   " +
                " cu.\"FirstName\",\r\n    cu.\"LastName\"\r\n   FROM dbo.\"Transaction\" tr\r\n   " +
                "  JOIN dbo.\"Account\" ac ON ac.\"Id\" = tr.\"AccountId\"\r\n   " +
                "  JOIN dbo.\"Customer\" cu ON cu.\"Id\" = ac.\"CustomerId\";\r\n");
				
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
