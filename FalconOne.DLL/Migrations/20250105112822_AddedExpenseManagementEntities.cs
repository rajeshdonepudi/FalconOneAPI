using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FalconOne.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpenseManagementEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountAlias",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20250105112821446) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20241201093409157) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceAlias",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20250105112821446) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20241201093409157) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))");

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastUpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_AspNetUsers_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_AspNetUsers_LastUpdatedByUserId",
                        column: x => x.LastUpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastUpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_AspNetUsers_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_AspNetUsers_LastUpdatedByUserId",
                        column: x => x.LastUpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_CreatedByUserId",
                table: "ExpenseCategories",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_DeletedByUserId",
                table: "ExpenseCategories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_LastUpdatedByUserId",
                table: "ExpenseCategories",
                column: "LastUpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CreatedByUserId",
                table: "Expenses",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_DeletedByUserId",
                table: "Expenses",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_LastUpdatedByUserId",
                table: "Expenses",
                column: "LastUpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.AlterColumn<string>(
                name: "AccountAlias",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20241201093409157) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20250105112821446) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceAlias",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20241201093409157) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20250105112821446) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))");
        }
    }
}
