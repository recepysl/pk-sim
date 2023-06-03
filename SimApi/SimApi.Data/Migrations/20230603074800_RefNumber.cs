using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimApi.Data.Migrations
{
    public partial class RefNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                schema: "dbo",
                table: "Transaction",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "TransactionCode",
                schema: "dbo",
                table: "Transaction",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionCode",
                schema: "dbo",
                table: "Transaction");

            migrationBuilder.AlterColumn<long>(
                name: "ReferenceNumber",
                schema: "dbo",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
