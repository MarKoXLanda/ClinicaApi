using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentrosCosto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Db2:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(32672)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(32672)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosCosto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insumos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Db2:Identity", "1, 1"),
                    SKU = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(32672)", nullable: false),
                    StockGlobal = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Db2:Identity", "1, 1"),
                    InsumoMedicoId = table.Column<int>(type: "int", nullable: false),
                    CentroCostoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaTransaccion = table.Column<DateTime>(type: "timestamp(6)", nullable: false),
                    UsuarioResponsable = table.Column<string>(type: "varchar(32672)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacciones_CentrosCosto_CentroCostoId",
                        column: x => x.CentroCostoId,
                        principalTable: "CentrosCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacciones_Insumos_InsumoMedicoId",
                        column: x => x.InsumoMedicoId,
                        principalTable: "Insumos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insumos_SKU",
                table: "Insumos",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CentroCostoId",
                table: "Transacciones",
                column: "CentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_InsumoMedicoId",
                table: "Transacciones",
                column: "InsumoMedicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "CentrosCosto");

            migrationBuilder.DropTable(
                name: "Insumos");
        }
    }
}
