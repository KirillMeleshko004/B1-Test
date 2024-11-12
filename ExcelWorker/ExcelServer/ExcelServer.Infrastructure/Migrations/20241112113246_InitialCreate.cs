using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TurnoverDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpeningBalanceAsset = table.Column<decimal>(type: "MONEY", nullable: false),
                    OpeningBalanceLiability = table.Column<decimal>(type: "MONEY", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "MONEY", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "MONEY", nullable: false),
                    ClosingBalanceAsset = table.Column<decimal>(type: "MONEY", nullable: false),
                    ClosingBalanceLiability = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnoverDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SummaryClass",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TurnoverDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpeningBalanceAsset = table.Column<decimal>(type: "MONEY", nullable: false),
                    OpeningBalanceLiability = table.Column<decimal>(type: "MONEY", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "MONEY", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "MONEY", nullable: false),
                    ClosingBalanceAsset = table.Column<decimal>(type: "MONEY", nullable: false),
                    ClosingBalanceLiability = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummaryClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SummaryClass_TurnoverDocument_TurnoverDocumentId",
                        column: x => x.TurnoverDocumentId,
                        principalTable: "TurnoverDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsSummary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    SummaryClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpeningBalanceAsset = table.Column<decimal>(type: "MONEY", nullable: false),
                    OpeningBalanceLiability = table.Column<decimal>(type: "MONEY", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "MONEY", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "MONEY", nullable: false),
                    ClosingBalanceAsset = table.Column<decimal>(type: "MONEY", nullable: false),
                    ClosingBalanceLiability = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsSummary_SummaryClass_SummaryClassId",
                        column: x => x.SummaryClassId,
                        principalTable: "SummaryClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    SummaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpeningBalanceAsset = table.Column<decimal>(type: "MONEY", nullable: false),
                    OpeningBalanceLiability = table.Column<decimal>(type: "MONEY", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "MONEY", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "MONEY", nullable: false),
                    ClosingBalanceAsset = table.Column<decimal>(type: "MONEY", nullable: false),
                    ClosingBalanceLiability = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountsSummary_SummaryId",
                        column: x => x.SummaryId,
                        principalTable: "AccountsSummary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_SummaryId",
                table: "Account",
                column: "SummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsSummary_SummaryClassId",
                table: "AccountsSummary",
                column: "SummaryClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SummaryClass_TurnoverDocumentId",
                table: "SummaryClass",
                column: "TurnoverDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "AccountsSummary");

            migrationBuilder.DropTable(
                name: "SummaryClass");

            migrationBuilder.DropTable(
                name: "TurnoverDocument");
        }
    }
}
