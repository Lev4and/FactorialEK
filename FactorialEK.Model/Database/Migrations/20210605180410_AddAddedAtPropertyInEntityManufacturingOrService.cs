using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FactorialEK.Model.Database.Migrations
{
    public partial class AddAddedAtPropertyInEntityManufacturingOrService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "ManufacturingOrServices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B867520A-92DB-4658-BE39-84DA53A601C0",
                column: "ConcurrencyStamp",
                value: "42591ac0-4b62-4a79-a89e-aa7b6f349725");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21F7B496-C675-4E8A-A34C-FC5EC0762FDB",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "933a6263-a3b0-4af2-a43f-a3fc3e9be520", "AQAAAAEAACcQAAAAEPO75Z6PfUGznUJPujDXXjrtwDyEM1kWljTc3vFRqYOLlri/+JjSHC9tDC1KLzrgbg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "ManufacturingOrServices");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B867520A-92DB-4658-BE39-84DA53A601C0",
                column: "ConcurrencyStamp",
                value: "2f6e580c-bb95-458f-9fb2-fe55e2eaf20b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21F7B496-C675-4E8A-A34C-FC5EC0762FDB",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3ee33bd0-ade9-4d93-97a5-187ee6df674e", "AQAAAAEAACcQAAAAEBSACdIGOzJO3cUviMOLhBMX57b2QiGxMruSXCDTxUcxh8fXLQSTYYH8YSmZgeyMNQ==" });
        }
    }
}
