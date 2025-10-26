using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "api_log_catalog_header",
                columns: table => new
                {
                    api_log_catalog_header_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_method = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    request_url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    response_code = table.Column<int>(type: "int", nullable: true),
                    id_tracking = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_log_catalog_header", x => x.api_log_catalog_header_id);
                });

            migrationBuilder.CreateTable(
                name: "catalog_error",
                columns: table => new
                {
                    catalog_error_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    error_description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    error_priority = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    error_status_code = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: true),
                    date_create = table.Column<DateTime>(type: "datetime2", nullable: true),
                    date_update = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalog_error", x => x.catalog_error_id);
                });

            migrationBuilder.CreateTable(
                name: "parametros",
                columns: table => new
                {
                    parametros_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_parametro = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    valor = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    date_create = table.Column<DateTime>(type: "datetime2", nullable: true),
                    date_update = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parametros", x => x.parametros_id);
                });

            migrationBuilder.CreateTable(
                name: "api_log_catalog_detail",
                columns: table => new
                {
                    api_log_catalog_detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    api_log_catalog_header_id = table.Column<int>(type: "int", nullable: true),
                    status_code = table.Column<int>(type: "int", nullable: true),
                    type_process = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    data_message = table.Column<string>(type: "text", maxLength: 300, nullable: true),
                    process_component = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_log_catalog_detail", x => x.api_log_catalog_detail_id);
                    table.ForeignKey(
                        name: "fk_catalog_header_detail",
                        column: x => x.api_log_catalog_header_id,
                        principalTable: "api_log_catalog_header",
                        principalColumn: "api_log_catalog_header_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_api_log_catalog_detail_api_log_catalog_header_id",
                table: "api_log_catalog_detail",
                column: "api_log_catalog_header_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "api_log_catalog_detail");

            migrationBuilder.DropTable(
                name: "catalog_error");

            migrationBuilder.DropTable(
                name: "parametros");

            migrationBuilder.DropTable(
                name: "api_log_catalog_header");
        }
    }
}
