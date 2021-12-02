using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domains_Scraper.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AllTimeOrganicData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllTimeOrganicData", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BacklinkType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TextLinks = table.Column<int>(type: "int", nullable: false),
                    FrameLinks = table.Column<int>(type: "int", nullable: false),
                    FormLinks = table.Column<int>(type: "int", nullable: false),
                    ImageLinks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BacklinkType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FollowLinksVsNoFollowLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FollowLinks = table.Column<int>(type: "int", nullable: false),
                    NotFollowLinks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowLinksVsNoFollowLink", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OneYearOrganicData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneYearOrganicData", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrganicData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganicTraffic = table.Column<int>(type: "int", nullable: false),
                    OrganicKeywords = table.Column<int>(type: "int", nullable: false),
                    OneYearOrganicDataId = table.Column<int>(type: "int", nullable: true),
                    AllTimeOrganicDataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganicData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganicData_AllTimeOrganicData_AllTimeOrganicDataId",
                        column: x => x.AllTimeOrganicDataId,
                        principalTable: "AllTimeOrganicData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganicData_OneYearOrganicData_OneYearOrganicDataId",
                        column: x => x.OneYearOrganicDataId,
                        principalTable: "OneYearOrganicData",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrganicTrafficChartData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OrganicTrafficValue = table.Column<int>(type: "int", nullable: false),
                    PaidTrafficValue = table.Column<int>(type: "int", nullable: false),
                    AllTimeOrganicDataId = table.Column<int>(type: "int", nullable: true),
                    OneYearOrganicDataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganicTrafficChartData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganicTrafficChartData_AllTimeOrganicData_AllTimeOrganicDat~",
                        column: x => x.AllTimeOrganicDataId,
                        principalTable: "AllTimeOrganicData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganicTrafficChartData_OneYearOrganicData_OneYearOrganicDat~",
                        column: x => x.OneYearOrganicDataId,
                        principalTable: "OneYearOrganicData",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrganicChartData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TopThree = table.Column<int>(type: "int", nullable: false),
                    FourToTen = table.Column<int>(type: "int", nullable: false),
                    ElevenToTwenty = table.Column<int>(type: "int", nullable: false),
                    TwentyOneToFifty = table.Column<int>(type: "int", nullable: false),
                    FiftyOneToOneHundred = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    AllTimeOrganicDataId = table.Column<int>(type: "int", nullable: true),
                    OneYearOrganicDataId = table.Column<int>(type: "int", nullable: true),
                    OrganicDataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganicChartData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganicChartData_AllTimeOrganicData_AllTimeOrganicDataId",
                        column: x => x.AllTimeOrganicDataId,
                        principalTable: "AllTimeOrganicData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganicChartData_OneYearOrganicData_OneYearOrganicDataId",
                        column: x => x.OneYearOrganicDataId,
                        principalTable: "OneYearOrganicData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganicChartData_OrganicData_OrganicDataId",
                        column: x => x.OrganicDataId,
                        principalTable: "OrganicData",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrganicTrafficAndKeywordsByCountry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OranicTraficValue = table.Column<int>(type: "int", nullable: false),
                    KeyWordsValue = table.Column<int>(type: "int", nullable: false),
                    OrganicDataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganicTrafficAndKeywordsByCountry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganicTrafficAndKeywordsByCountry_OrganicData_OrganicDataId",
                        column: x => x.OrganicDataId,
                        principalTable: "OrganicData",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SemrushDomain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthorityScore = table.Column<int>(type: "int", nullable: false),
                    Backlinks = table.Column<int>(type: "int", nullable: false),
                    OrganicDataId = table.Column<int>(type: "int", nullable: true),
                    FollowLinksVsNotFollowLinkId = table.Column<int>(type: "int", nullable: true),
                    BacklinkTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemrushDomain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemrushDomain_BacklinkType_BacklinkTypeId",
                        column: x => x.BacklinkTypeId,
                        principalTable: "BacklinkType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SemrushDomain_FollowLinksVsNoFollowLink_FollowLinksVsNotFoll~",
                        column: x => x.FollowLinksVsNotFollowLinkId,
                        principalTable: "FollowLinksVsNoFollowLink",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SemrushDomain_OrganicData_OrganicDataId",
                        column: x => x.OrganicDataId,
                        principalTable: "OrganicData",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrganicChartData_AllTimeOrganicDataId",
                table: "OrganicChartData",
                column: "AllTimeOrganicDataId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganicChartData_OneYearOrganicDataId",
                table: "OrganicChartData",
                column: "OneYearOrganicDataId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganicChartData_OrganicDataId",
                table: "OrganicChartData",
                column: "OrganicDataId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganicData_AllTimeOrganicDataId",
                table: "OrganicData",
                column: "AllTimeOrganicDataId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganicData_OneYearOrganicDataId",
                table: "OrganicData",
                column: "OneYearOrganicDataId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganicTrafficAndKeywordsByCountry_OrganicDataId",
                table: "OrganicTrafficAndKeywordsByCountry",
                column: "OrganicDataId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganicTrafficChartData_AllTimeOrganicDataId",
                table: "OrganicTrafficChartData",
                column: "AllTimeOrganicDataId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganicTrafficChartData_OneYearOrganicDataId",
                table: "OrganicTrafficChartData",
                column: "OneYearOrganicDataId");

            migrationBuilder.CreateIndex(
                name: "IX_SemrushDomain_BacklinkTypeId",
                table: "SemrushDomain",
                column: "BacklinkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SemrushDomain_FollowLinksVsNotFollowLinkId",
                table: "SemrushDomain",
                column: "FollowLinksVsNotFollowLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_SemrushDomain_OrganicDataId",
                table: "SemrushDomain",
                column: "OrganicDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganicChartData");

            migrationBuilder.DropTable(
                name: "OrganicTrafficAndKeywordsByCountry");

            migrationBuilder.DropTable(
                name: "OrganicTrafficChartData");

            migrationBuilder.DropTable(
                name: "SemrushDomain");

            migrationBuilder.DropTable(
                name: "BacklinkType");

            migrationBuilder.DropTable(
                name: "FollowLinksVsNoFollowLink");

            migrationBuilder.DropTable(
                name: "OrganicData");

            migrationBuilder.DropTable(
                name: "AllTimeOrganicData");

            migrationBuilder.DropTable(
                name: "OneYearOrganicData");
        }
    }
}
