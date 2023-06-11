using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimApi.Data.Migrations
{
    public partial class InitialMigrationSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("" +
      "\r\nINSERT INTO dbo.\"Currency\" (\"CreatedBy\",\"CreatedAt\",\"Name\", \"Code\", \"Symbol\", \"IsActive\") VALUES ('migration', '2023-05-21 10:58:46.377331+03','Dollars', 'USD', '$',true);\r\n" +
          "INSERT INTO dbo.\"Currency\" (\"CreatedBy\",\"CreatedAt\",\"Name\", \"Code\", \"Symbol\", \"IsActive\") VALUES ('migration', '2023-05-21 10:58:46.377331+03','Euro', 'EUR', '€',true);\r\n" +
          "INSERT INTO dbo.\"Currency\" (\"CreatedBy\",\"CreatedAt\",\"Name\", \"Code\", \"Symbol\", \"IsActive\") VALUES ('migration', '2023-05-21 10:58:46.377331+03','Pesos', 'CUP', '₱',true);\r\n" +
          "INSERT INTO dbo.\"Currency\" (\"CreatedBy\",\"CreatedAt\",\"Name\", \"Code\", \"Symbol\", \"IsActive\") VALUES ('migration', '2023-05-21 10:58:46.377331+03','Liras', 'TRL', '£',true);\r\n" +
          "INSERT INTO dbo.\"Currency\" (\"CreatedBy\",\"CreatedAt\",\"Name\", \"Code\", \"Symbol\", \"IsActive\") VALUES ('migration', '2023-05-21 10:58:46.377331+03','Zimbabwe Dollars', 'ZWD', 'Z$',true);"
          );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
