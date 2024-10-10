using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pricingModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_pricingModels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pricingModels");
        }
    }
}
