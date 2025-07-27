using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sheraton.Migrations
{
    /// <inheritdoc />
    public partial class ghichu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "HopDongs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "HopDongs");
        }
    }
}
