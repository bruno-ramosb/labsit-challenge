using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labsit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FINANCE");

            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Document = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BANKACCOUNT",
                schema: "FINANCE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BRANCHCODE = table.Column<string>(type: "TEXT", nullable: false),
                    ACCOUNTNUMBER = table.Column<string>(type: "TEXT", nullable: false),
                    BALANCE = table.Column<decimal>(type: "TEXT", nullable: false),
                    TOTALCREDITLIMIT = table.Column<decimal>(type: "TEXT", nullable: false),
                    AVAILABLECREDITLIMIT = table.Column<decimal>(type: "TEXT", nullable: false),
                    IDCUSTOMER = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BANKACCOUNT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BANKACCOUNT_CUSTOMER_IDCUSTOMER",
                        column: x => x.IDCUSTOMER,
                        principalSchema: "FINANCE",
                        principalTable: "CUSTOMER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CARD",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    HolderName = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Brand = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    VerificationCode = table.Column<string>(type: "TEXT", nullable: false),
                    BankAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CARD_BANKACCOUNT_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "FINANCE",
                        principalTable: "BANKACCOUNT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BANKACCOUNT_IDCUSTOMER",
                schema: "FINANCE",
                table: "BANKACCOUNT",
                column: "IDCUSTOMER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CARD_BankAccountId",
                schema: "FINANCE",
                table: "CARD",
                column: "BankAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CARD",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "BANKACCOUNT",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "CUSTOMER",
                schema: "FINANCE");
        }
    }
}
