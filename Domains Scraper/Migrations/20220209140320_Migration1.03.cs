using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domains_Scraper.Migrations
{
    public partial class Migration103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SemrushDomain_BacklinkType_BacklinkTypeId",
                table: "SemrushDomain");

            migrationBuilder.DropForeignKey(
                name: "FK_SemrushDomain_FollowLinksVsNoFollowLink_FollowLinksVsNotFoll~",
                table: "SemrushDomain");

            migrationBuilder.DropForeignKey(
                name: "FK_SemrushDomain_OrganicData_OrganicDataId",
                table: "SemrushDomain");

            migrationBuilder.AlterColumn<int>(
                name: "OrganicDataId",
                table: "SemrushDomain",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FollowLinksVsNotFollowLinkId",
                table: "SemrushDomain",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Backlinks",
                table: "SemrushDomain",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BacklinkTypeId",
                table: "SemrushDomain",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AuthorityScore",
                table: "SemrushDomain",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "PaidTrafficValue",
                table: "OrganicTrafficChartData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "OrganicTrafficValue",
                table: "OrganicTrafficChartData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "OranicTraficValue",
                table: "OrganicTrafficAndKeywordsByCountry",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "KeyWordsValue",
                table: "OrganicTrafficAndKeywordsByCountry",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "OrganicTraffic",
                table: "OrganicData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "OrganicKeywords",
                table: "OrganicData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "TwentyOneToFifty",
                table: "OrganicChartData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Total",
                table: "OrganicChartData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "TopThree",
                table: "OrganicChartData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "FourToTen",
                table: "OrganicChartData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "FiftyOneToOneHundred",
                table: "OrganicChartData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "ElevenToTwenty",
                table: "OrganicChartData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "NotFollowLinks",
                table: "FollowLinksVsNoFollowLink",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "FollowLinks",
                table: "FollowLinksVsNoFollowLink",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "TextLinks",
                table: "BacklinkType",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "ImageLinks",
                table: "BacklinkType",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "FrameLinks",
                table: "BacklinkType",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "FormLinks",
                table: "BacklinkType",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SemrushDomain_BacklinkType_BacklinkTypeId",
                table: "SemrushDomain",
                column: "BacklinkTypeId",
                principalTable: "BacklinkType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemrushDomain_FollowLinksVsNoFollowLink_FollowLinksVsNotFoll~",
                table: "SemrushDomain",
                column: "FollowLinksVsNotFollowLinkId",
                principalTable: "FollowLinksVsNoFollowLink",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemrushDomain_OrganicData_OrganicDataId",
                table: "SemrushDomain",
                column: "OrganicDataId",
                principalTable: "OrganicData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SemrushDomain_BacklinkType_BacklinkTypeId",
                table: "SemrushDomain");

            migrationBuilder.DropForeignKey(
                name: "FK_SemrushDomain_FollowLinksVsNoFollowLink_FollowLinksVsNotFoll~",
                table: "SemrushDomain");

            migrationBuilder.DropForeignKey(
                name: "FK_SemrushDomain_OrganicData_OrganicDataId",
                table: "SemrushDomain");

            migrationBuilder.AlterColumn<int>(
                name: "OrganicDataId",
                table: "SemrushDomain",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FollowLinksVsNotFollowLinkId",
                table: "SemrushDomain",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Backlinks",
                table: "SemrushDomain",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "BacklinkTypeId",
                table: "SemrushDomain",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorityScore",
                table: "SemrushDomain",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "PaidTrafficValue",
                table: "OrganicTrafficChartData",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "OrganicTrafficValue",
                table: "OrganicTrafficChartData",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "OranicTraficValue",
                table: "OrganicTrafficAndKeywordsByCountry",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "KeyWordsValue",
                table: "OrganicTrafficAndKeywordsByCountry",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "OrganicTraffic",
                table: "OrganicData",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "OrganicKeywords",
                table: "OrganicData",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "TwentyOneToFifty",
                table: "OrganicChartData",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Total",
                table: "OrganicChartData",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "TopThree",
                table: "OrganicChartData",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "FourToTen",
                table: "OrganicChartData",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "FiftyOneToOneHundred",
                table: "OrganicChartData",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "ElevenToTwenty",
                table: "OrganicChartData",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "NotFollowLinks",
                table: "FollowLinksVsNoFollowLink",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "FollowLinks",
                table: "FollowLinksVsNoFollowLink",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "TextLinks",
                table: "BacklinkType",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "ImageLinks",
                table: "BacklinkType",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "FrameLinks",
                table: "BacklinkType",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "FormLinks",
                table: "BacklinkType",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_SemrushDomain_BacklinkType_BacklinkTypeId",
                table: "SemrushDomain",
                column: "BacklinkTypeId",
                principalTable: "BacklinkType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SemrushDomain_FollowLinksVsNoFollowLink_FollowLinksVsNotFoll~",
                table: "SemrushDomain",
                column: "FollowLinksVsNotFollowLinkId",
                principalTable: "FollowLinksVsNoFollowLink",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SemrushDomain_OrganicData_OrganicDataId",
                table: "SemrushDomain",
                column: "OrganicDataId",
                principalTable: "OrganicData",
                principalColumn: "Id");
        }
    }
}
