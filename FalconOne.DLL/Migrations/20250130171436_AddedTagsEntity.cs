using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FalconOne.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedTagsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountAlias",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20250130171435072) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20250105112821446) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceAlias",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20250130171435072) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20250105112821446) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_AspNetUsers_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_AspNetUsers_LastUpdatedByUserId",
                        column: x => x.LastUpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastUpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityTag_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTag_AspNetUsers_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTag_AspNetUsers_LastUpdatedByUserId",
                        column: x => x.LastUpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTag_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityTag_CreatedByUserId",
                table: "EntityTag",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTag_DeletedByUserId",
                table: "EntityTag",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTag_LastUpdatedByUserId",
                table: "EntityTag",
                column: "LastUpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTag_TagId",
                table: "EntityTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTag_UserId",
                table: "EntityTag",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CreatedByUserId",
                table: "Tag",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_DeletedByUserId",
                table: "Tag",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_LastUpdatedByUserId",
                table: "Tag",
                column: "LastUpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.AlterColumn<string>(
                name: "AccountAlias",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20250105112821446) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20250130171435072) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceAlias",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20250105112821446) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20250130171435072) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))");
        }
    }
}
