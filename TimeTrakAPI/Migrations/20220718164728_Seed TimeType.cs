using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrakAPI.Migrations
{
    public partial class SeedTimeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TimeTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Minutes" },
                    { 2, "Hours" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimeTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TimeTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
