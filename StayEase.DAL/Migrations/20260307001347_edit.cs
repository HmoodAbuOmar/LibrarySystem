using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayEase.DAL.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "language",
                table: "HotelTranslations",
                newName: "Language");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "HotelTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Language",
                table: "HotelTranslations",
                newName: "language");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "HotelTranslations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
