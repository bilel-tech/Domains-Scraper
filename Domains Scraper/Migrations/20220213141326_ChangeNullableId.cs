using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domains_Scraper.Migrations
{
    public partial class ChangeNullableId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganicTrafficChartData_AllTimeOrganicData_AllTimeOrganicDat~",
                table: "OrganicTrafficChartData");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganicTrafficChartData_OneYearOrganicData_OneYearOrganicDat~",
                table: "OrganicTrafficChartData");

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
                name: "BacklinkTypeId",
                table: "SemrushDomain",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OneYearOrganicDataId",
                table: "OrganicTrafficChartData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AllTimeOrganicDataId",
                table: "OrganicTrafficChartData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganicTrafficChartData_AllTimeOrganicData_AllTimeOrganicDat~",
                table: "OrganicTrafficChartData",
                column: "AllTimeOrganicDataId",
                principalTable: "AllTimeOrganicData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganicTrafficChartData_OneYearOrganicData_OneYearOrganicDat~",
                table: "OrganicTrafficChartData",
                column: "OneYearOrganicDataId",
                principalTable: "OneYearOrganicData",
                principalColumn: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganicTrafficChartData_AllTimeOrganicData_AllTimeOrganicDat~",
                table: "OrganicTrafficChartData");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganicTrafficChartData_OneYearOrganicData_OneYearOrganicDat~",
                table: "OrganicTrafficChartData");

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

            migrationBuilder.AlterColumn<int>(
                name: "BacklinkTypeId",
                table: "SemrushDomain",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OneYearOrganicDataId",
                table: "OrganicTrafficChartData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AllTimeOrganicDataId",
                table: "OrganicTrafficChartData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganicTrafficChartData_AllTimeOrganicData_AllTimeOrganicDat~",
                table: "OrganicTrafficChartData",
                column: "AllTimeOrganicDataId",
                principalTable: "AllTimeOrganicData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganicTrafficChartData_OneYearOrganicData_OneYearOrganicDat~",
                table: "OrganicTrafficChartData",
                column: "OneYearOrganicDataId",
                principalTable: "OneYearOrganicData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
