using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domains_Scraper.Migrations
{
    public partial class RemoneUnnecessaryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SemrushDomainId",
                table: "FollowLinksVsNoFollowLink");

            migrationBuilder.DropColumn(
                name: "SemrushDomainId",
                table: "BacklinkType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SemrushDomainId",
                table: "FollowLinksVsNoFollowLink",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SemrushDomainId",
                table: "BacklinkType",
                type: "int",
                nullable: true);
        }
    }
}
