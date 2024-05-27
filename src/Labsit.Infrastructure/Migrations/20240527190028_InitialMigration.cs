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
                name: "Finance");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Finance",
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
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BranchCode = table.Column<string>(type: "TEXT", nullable: false),
                    AccountNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCreditLimit = table.Column<decimal>(type: "TEXT", nullable: false),
                    AvailableCreditLimit = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccount_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Finance",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    HolderName = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Brand = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    VerificationCode = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    BankAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "Finance",
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_CustomerId",
                schema: "Finance",
                table: "BankAccount",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_BankAccountId",
                schema: "Finance",
                table: "Card",
                column: "BankAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "BankAccount",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Finance");
        }
    }
}
