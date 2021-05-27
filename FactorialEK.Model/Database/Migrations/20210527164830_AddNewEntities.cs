using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FactorialEK.Model.Database.Migrations
{
    public partial class AddNewEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriesManufacturingOrService",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesManufacturingOrService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryPhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPhotosManufacturingOrService",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryManufacturingOrServiceId = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPhotosManufacturingOrService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryPhotosManufacturingOrService_CategoriesManufacturingOrService_CategoryManufacturingOrServiceId",
                        column: x => x.CategoryManufacturingOrServiceId,
                        principalTable: "CategoriesManufacturingOrService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturingOrServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryManufacturingOrServiceId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingOrServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturingOrServices_CategoriesManufacturingOrService_CategoryManufacturingOrServiceId",
                        column: x => x.CategoryManufacturingOrServiceId,
                        principalTable: "CategoriesManufacturingOrService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturingOrServiceInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ManufacturingOrServiceId = table.Column<Guid>(nullable: false),
                    DescriptionOfValueFormation = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: false),
                    Specifications = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Advantages = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingOrServiceInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturingOrServiceInformation_ManufacturingOrServices_ManufacturingOrServiceId",
                        column: x => x.ManufacturingOrServiceId,
                        principalTable: "ManufacturingOrServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturingOrServiceMainPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ManufacturingOrServiceId = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingOrServiceMainPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturingOrServiceMainPhotos_ManufacturingOrServices_ManufacturingOrServiceId",
                        column: x => x.ManufacturingOrServiceId,
                        principalTable: "ManufacturingOrServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturingOrServicePhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ManufacturingOrServiceId = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingOrServicePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturingOrServicePhotos_ManufacturingOrServices_ManufacturingOrServiceId",
                        column: x => x.ManufacturingOrServiceId,
                        principalTable: "ManufacturingOrServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturingOrServicePrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ManufacturingOrServiceId = table.Column<Guid>(nullable: false),
                    SecondPrice = table.Column<int>(nullable: true),
                    LowerLimitPrice = table.Column<int>(nullable: true),
                    UpperLimitPrice = table.Column<int>(nullable: true),
                    IsUponRequest = table.Column<bool>(nullable: false),
                    IsIndividualCalculation = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingOrServicePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturingOrServicePrices_ManufacturingOrServices_ManufacturingOrServiceId",
                        column: x => x.ManufacturingOrServiceId,
                        principalTable: "ManufacturingOrServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPhotosManufacturingOrService_CategoryManufacturingOrServiceId",
                table: "CategoryPhotosManufacturingOrService",
                column: "CategoryManufacturingOrServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingOrServiceInformation_ManufacturingOrServiceId",
                table: "ManufacturingOrServiceInformation",
                column: "ManufacturingOrServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingOrServiceMainPhotos_ManufacturingOrServiceId",
                table: "ManufacturingOrServiceMainPhotos",
                column: "ManufacturingOrServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingOrServicePhotos_ManufacturingOrServiceId",
                table: "ManufacturingOrServicePhotos",
                column: "ManufacturingOrServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingOrServicePrices_ManufacturingOrServiceId",
                table: "ManufacturingOrServicePrices",
                column: "ManufacturingOrServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingOrServices_CategoryManufacturingOrServiceId",
                table: "ManufacturingOrServices",
                column: "CategoryManufacturingOrServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPhotosManufacturingOrService");

            migrationBuilder.DropTable(
                name: "GalleryPhotos");

            migrationBuilder.DropTable(
                name: "ManufacturingOrServiceInformation");

            migrationBuilder.DropTable(
                name: "ManufacturingOrServiceMainPhotos");

            migrationBuilder.DropTable(
                name: "ManufacturingOrServicePhotos");

            migrationBuilder.DropTable(
                name: "ManufacturingOrServicePrices");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "ManufacturingOrServices");

            migrationBuilder.DropTable(
                name: "CategoriesManufacturingOrService");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B867520A-92DB-4658-BE39-84DA53A601C0",
                column: "ConcurrencyStamp",
                value: "0a0c2619-5199-4985-a010-1a10b5e45727");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21F7B496-C675-4E8A-A34C-FC5EC0762FDB",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b5d7d9a9-f049-4545-83dc-7e06e52bda3b", "AQAAAAEAACcQAAAAEMOior94d5kCOgz7nYRIhZwb+ByriWDR4CsZ63XZaBVheWWMdkupqmEPN4iMDXbL7w==" });
        }
    }
}
