using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteForum.Data.Migrations
{
    /// <inheritdoc />
    public partial class demo_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBan",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBan",
                table: "AspNetUsers");
        }
    }
}
