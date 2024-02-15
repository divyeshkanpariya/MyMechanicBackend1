using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMechanic.Entities.Migrations
{
    /// <inheritdoc />
    public partial class RemovePostalCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garage_PostalCode_PostalCodeId",
                table: "Garage");

            migrationBuilder.DropTable(
                name: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "PostalCodeId",
                table: "Garage",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Garage_PostalCodeId",
                table: "Garage",
                newName: "IX_Garage_CityId");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Garage",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Garage_City_CityId",
                table: "Garage",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garage_City_CityId",
                table: "Garage");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Garage");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Garage",
                newName: "PostalCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Garage_CityId",
                table: "Garage",
                newName: "IX_Garage_PostalCodeId");

            migrationBuilder.CreateTable(
                name: "PostalCode",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostalCode_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostalCode_CityId",
                table: "PostalCode",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Garage_PostalCode_PostalCodeId",
                table: "Garage",
                column: "PostalCodeId",
                principalTable: "PostalCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
