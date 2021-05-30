using Microsoft.EntityFrameworkCore.Migrations;

namespace FactorialEK.Model.Database.Migrations
{
    public partial class ChangeEntityNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B867520A-92DB-4658-BE39-84DA53A601C0",
                column: "ConcurrencyStamp",
                value: "9642be2c-fa46-482c-a8c5-4218f2e3ba51");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21F7B496-C675-4E8A-A34C-FC5EC0762FDB",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "02079772-d6a1-4ec3-8b45-da24d8f2202f", "AQAAAAEAACcQAAAAEL4YVh3btXBxcPk+KLs+xAFYNjC4uYaNOA4wufLjP5nuc1dWV5kYmG6QUj8cz0cY2g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B867520A-92DB-4658-BE39-84DA53A601C0",
                column: "ConcurrencyStamp",
                value: "ba129dd2-4502-4d5c-8ee9-528a1abd0db9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21F7B496-C675-4E8A-A34C-FC5EC0762FDB",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "424e2601-55ef-4d3c-a992-fc29cc366b34", "AQAAAAEAACcQAAAAEOd7pkM0EDPyuXk4ffz9vU6V2DAKotG8lmr5hNgcU+R0R8Vj+qvnWinviAT865a+Pg==" });
        }
    }
}
