using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BluegrassDigitalPeopleDirectory.Data.migrations
{
    /// <inheritdoc />
    public partial class peopledirectory4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureFormat",
                table: "ProfilePictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureFormat",
                table: "ProfilePictures");
        }
    }
}
