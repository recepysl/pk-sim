using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimApi.Data.Migrations
{
    public partial class CurrencySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("" +
                "\r\nINSERT INTO dbo.\"Currency\" (\"CreatedBy\",\"CreatedAt\",\"Name\", \"Code\", \"Symbol\") VALUES ('migration', '2023-05-21 10:58:46.377331+03','Dollars', 'USD', '$');\r\n" +
                "INSERT INTO dbo.\"Currency\" (\"CreatedBy\",\"CreatedAt\",\"Name\", \"Code\", \"Symbol\") VALUES ('migration', '2023-05-21 10:58:46.377331+03','Euro', 'EUR', '€');\r\n" +
                "INSERT INTO dbo.\"Currency\" (\"CreatedBy\",\"CreatedAt\",\"Name\", \"Code\", \"Symbol\") VALUES ('migration', '2023-05-21 10:58:46.377331+03','Pesos', 'CUP', '₱');\r\n" +
                "INSERT INTO dbo.\"Currency\" (\"CreatedBy\",\"CreatedAt\",\"Name\", \"Code\", \"Symbol\") VALUES ('migration', '2023-05-21 10:58:46.377331+03','Liras', 'TRL', '£');\r\n" +
                "INSERT INTO dbo.\"Currency\" (\"CreatedBy\",\"CreatedAt\",\"Name\", \"Code\", \"Symbol\") VALUES ('migration', '2023-05-21 10:58:46.377331+03','Zimbabwe Dollars', 'ZWD', 'Z$');"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
