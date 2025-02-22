using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FalconOne.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EnhancedTaggingSystem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedFrom",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AccountAlias",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20250221193607101) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20250221192930641) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceAlias",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20250221193607101) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20250221192930641) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedFrom",
                table: "Tag");

            migrationBuilder.AlterColumn<string>(
                name: "AccountAlias",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20250221192930641) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20250221193607101) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceAlias",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONVERT(NVARCHAR(max), 20250221192930641) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONVERT(NVARCHAR(max), 20250221193607101) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))");
        }
    }
}
