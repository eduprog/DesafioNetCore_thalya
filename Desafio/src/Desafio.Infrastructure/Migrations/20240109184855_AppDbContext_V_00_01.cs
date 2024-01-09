using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AppDbContext_V_00_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "person",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    document = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    enable = table.Column<bool>(type: "boolean", nullable: false),
                    can_sell = table.Column<bool>(type: "boolean", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: false),
                    alternative_code = table.Column<string>(type: "text", nullable: false),
                    short_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unit",
                schema: "public",
                columns: table => new
                {
                    acronym = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    short_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit", x => x.acronym);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    short_description = table.Column<string>(type: "text", nullable: false),
                    acronym = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric(15,4)", precision: 15, scale: 4, nullable: false),
                    stored_quantity = table.Column<decimal>(type: "numeric(15,4)", precision: 15, scale: 4, nullable: false),
                    enable = table.Column<bool>(type: "boolean", nullable: false),
                    sellable = table.Column<bool>(type: "boolean", nullable: false),
                    bar_code = table.Column<string>(type: "text", nullable: false),
                    short_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_unit_acronym",
                        column: x => x.acronym,
                        principalSchema: "public",
                        principalTable: "unit",
                        principalColumn: "acronym",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_acronym",
                schema: "public",
                table: "product",
                column: "acronym");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person",
                schema: "public");

            migrationBuilder.DropTable(
                name: "product",
                schema: "public");

            migrationBuilder.DropTable(
                name: "unit",
                schema: "public");
        }
    }
}
