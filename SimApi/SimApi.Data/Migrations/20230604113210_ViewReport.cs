using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimApi.Data.Migrations
{
    public partial class ViewReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"" +
                $"create view dbo.\"vTransactionReport\" as \r\nselect  " +
                $"tr.\"Id\",tr.\"AccountId\",tr.\"Amount\",tr.\"Direction\"," +
                $"tr.\"TransactionDate\",tr.\"Description\"," +
                $"\r\ntr.\"ReferenceNumber\",tr.\"TransactionCode\"," +
                $" \r\nac.\"Name\" as \"AccountName\" , ac.\"AccountNumber\"," +
                $"ac.\"CustomerId\",\r\ncu.\"CustomerNumber\", cu.\"FirstName\" ," +
                $"cu.\"LastName\"\r\nfrom dbo.\"Transaction\" tr\r\n" +
                $"inner join dbo.\"Account\" ac on ac.\"Id\" = tr.\"AccountId\"\r\n" +
                $"inner join dbo.\"Customer\" cu on cu.\"Id\" = ac.\"CustomerId\"");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop view dbo.\"vTransactionReport\"");
        }
    }
}
