using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectionCycles",
                columns: table => new
                {
                    Year = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartOn = table.Column<DateOnly>(type: "date", nullable: false),
                    EndOn = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionCycles", x => x.Year);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FipsCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElectralVotes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ElectionCycleVotes",
                columns: table => new
                {
                    ElectionCycle = table.Column<int>(type: "int", nullable: false),
                    FipsCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Votes = table.Column<long>(type: "bigint", nullable: false),
                    ElectionCycleYear = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionCycleVotes", x => new { x.ElectionCycle, x.FipsCode });
                    table.ForeignKey(
                        name: "FK_ElectionCycleVotes_ElectionCycles_ElectionCycleYear",
                        column: x => x.ElectionCycleYear,
                        principalTable: "ElectionCycles",
                        principalColumn: "Year");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCycleVotes_ElectionCycleYear",
                table: "ElectionCycleVotes",
                column: "ElectionCycleYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectionCycleVotes");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "ElectionCycles");
        }
    }
}
