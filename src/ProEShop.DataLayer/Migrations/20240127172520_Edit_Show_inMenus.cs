using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Edit_Show_inMenus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SHowInMenus",
                table: "Categories",
                newName: "ShowInMenus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShowInMenus",
                table: "Categories",
                newName: "SHowInMenus");
        }
    }
}
