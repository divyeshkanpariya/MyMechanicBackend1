using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMechanic.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddGarageMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GarageId",
                table: "GarageAvailService",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "GarageMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarageMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarageMedia_Garage_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarageAvailService_GarageId",
                table: "GarageAvailService",
                column: "GarageId");

            migrationBuilder.CreateIndex(
                name: "IX_GarageMedia_GarageId",
                table: "GarageMedia",
                column: "GarageId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarageAvailService_Garage_GarageId",
                table: "GarageAvailService",
                column: "GarageId",
                principalTable: "Garage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarageAvailService_Garage_GarageId",
                table: "GarageAvailService");

            migrationBuilder.DropTable(
                name: "GarageMedia");

            migrationBuilder.DropIndex(
                name: "IX_GarageAvailService_GarageId",
                table: "GarageAvailService");

            migrationBuilder.DropColumn(
                name: "GarageId",
                table: "GarageAvailService");
        }
    }
}
