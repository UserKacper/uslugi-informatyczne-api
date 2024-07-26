using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dynamicWPInformationModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    PricingMin = table.Column<double>(type: "double precision", nullable: false),
                    PricingCurrency = table.Column<string>(type: "text", nullable: false),
                    FullyCustomizable = table.Column<string>(type: "text", nullable: false),
                    HelpWithIntegration = table.Column<string>(type: "text", nullable: false),
                    Documentation = table.Column<string>(type: "text", nullable: false),
                    Emergency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dynamicWPInformationModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "smarthomeInformationModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    PricingMin = table.Column<double>(type: "double precision", nullable: false),
                    PricingCurrency = table.Column<string>(type: "text", nullable: false),
                    PanelAdminDesc = table.Column<string>(type: "text", nullable: false),
                    MobileAccessControl = table.Column<string>(type: "text", nullable: false),
                    HelpWithIntegration = table.Column<string>(type: "text", nullable: false),
                    Documentation = table.Column<string>(type: "text", nullable: false),
                    Emergency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smarthomeInformationModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "softwareInformationModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    PricingMin = table.Column<double>(type: "double precision", nullable: false),
                    PricingCurrency = table.Column<string>(type: "text", nullable: false),
                    FullyCustomizable = table.Column<string>(type: "text", nullable: false),
                    HelpWithIntegration = table.Column<string>(type: "text", nullable: false),
                    Documentation = table.Column<string>(type: "text", nullable: false),
                    Emergency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_softwareInformationModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "staticWPInformationModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    PricingMin = table.Column<double>(type: "double precision", nullable: false),
                    PricingCurrency = table.Column<string>(type: "text", nullable: false),
                    FullyCustomizable = table.Column<string>(type: "text", nullable: false),
                    HelpWithIntegration = table.Column<string>(type: "text", nullable: false),
                    Documentation = table.Column<string>(type: "text", nullable: false),
                    Emergency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staticWPInformationModels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dynamicWPInformationModels");

            migrationBuilder.DropTable(
                name: "smarthomeInformationModel");

            migrationBuilder.DropTable(
                name: "softwareInformationModels");

            migrationBuilder.DropTable(
                name: "staticWPInformationModels");
        }
    }
}
