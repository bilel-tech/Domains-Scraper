using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domains_Scraper.Migrations
{
    public partial class ChangeReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganicData_AllTimeOrganicData_AllTimeOrganicDataId",
                table: "OrganicData");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganicData_OneYearOrganicData_OneYearOrganicDataId",
                table: "OrganicData");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganicTrafficChartData_AllTimeOrganicData_AllTimeOrganicDat~",
                table: "OrganicTrafficChartData");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganicTrafficChartData_OneYearOrganicData_OneYearOrganicDat~",
                table: "OrganicTrafficChartData");

            migrationBuilder.DropIndex(
                name: "IX_OrganicData_AllTimeOrganicDataId",
                table: "OrganicData");

            migrationBuilder.DropIndex(
                name: "IX_OrganicData_OneYearOrganicDataId",
                table: "OrganicData");

            migrationBuilder.DropColumn(
                name: "AllTimeOrganicDataId",
                table: "OrganicData");

            migrationBuilder.DropColumn(
                name: "OneYearOrganicDataId",
                table: "OrganicData");

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

            migrationBuilder.AddColumn<int>(
                name: "OrganicDataId",
                table: "OneYearOrganicData",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "OrganicDataId",
                table: "AllTimeOrganicData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OneYearOrganicData_OrganicDataId",
                table: "OneYearOrganicData",
                column: "OrganicDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllTimeOrganicData_OrganicDataId",
                table: "AllTimeOrganicData",
                column: "OrganicDataId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AllTimeOrganicData_OrganicData_OrganicDataId",
                table: "AllTimeOrganicData",
                column: "OrganicDataId",
                principalTable: "OrganicData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OneYearOrganicData_OrganicData_OrganicDataId",
                table: "OneYearOrganicData",
                column: "OrganicDataId",
                principalTable: "OrganicData",
                principalColumn: "Id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllTimeOrganicData_OrganicData_OrganicDataId",
                table: "AllTimeOrganicData");

            migrationBuilder.DropForeignKey(
                name: "FK_OneYearOrganicData_OrganicData_OrganicDataId",
                table: "OneYearOrganicData");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganicTrafficChartData_AllTimeOrganicData_AllTimeOrganicDat~",
                table: "OrganicTrafficChartData");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganicTrafficChartData_OneYearOrganicData_OneYearOrganicDat~",
                table: "OrganicTrafficChartData");

            migrationBuilder.DropIndex(
                name: "IX_OneYearOrganicData_OrganicDataId",
                table: "OneYearOrganicData");

            migrationBuilder.DropIndex(
                name: "IX_AllTimeOrganicData_OrganicDataId",
                table: "AllTimeOrganicData");

            migrationBuilder.DropColumn(
                name: "OrganicDataId",
                table: "OneYearOrganicData");

            migrationBuilder.DropColumn(
                name: "SemrushDomainId",
                table: "FollowLinksVsNoFollowLink");

            migrationBuilder.DropColumn(
                name: "SemrushDomainId",
                table: "BacklinkType");

            migrationBuilder.DropColumn(
                name: "OrganicDataId",
                table: "AllTimeOrganicData");

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

            migrationBuilder.AddColumn<int>(
                name: "AllTimeOrganicDataId",
                table: "OrganicData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OneYearOrganicDataId",
                table: "OrganicData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganicData_AllTimeOrganicDataId",
                table: "OrganicData",
                column: "AllTimeOrganicDataId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganicData_OneYearOrganicDataId",
                table: "OrganicData",
                column: "OneYearOrganicDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganicData_AllTimeOrganicData_AllTimeOrganicDataId",
                table: "OrganicData",
                column: "AllTimeOrganicDataId",
                principalTable: "AllTimeOrganicData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganicData_OneYearOrganicData_OneYearOrganicDataId",
                table: "OrganicData",
                column: "OneYearOrganicDataId",
                principalTable: "OneYearOrganicData",
                principalColumn: "Id");

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
        }
    }
}
