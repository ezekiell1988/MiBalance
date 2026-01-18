using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiBalance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaPadreId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_CategoriaPadreId",
                        column: x => x.CategoriaPadreId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CuentasContables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Naturaleza = table.Column<int>(type: "int", nullable: false),
                    CuentaPadreId = table.Column<int>(type: "int", nullable: true),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    EsDeMovimiento = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasContables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuentasContables_CuentasContables_CuentaPadreId",
                        column: x => x.CuentaPadreId,
                        principalTable: "CuentasContables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RUC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UltimosDigitos = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Banco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LimiteCredito = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiaCorte = table.Column<int>(type: "int", nullable: false),
                    DiaPago = table.Column<int>(type: "int", nullable: false),
                    Titular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CuentasPorCobrar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    FechaPrestamo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoOriginal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoPendiente = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TasaInteres = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasPorCobrar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuentasPorCobrar_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CuentasPorPagar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarjetaId = table.Column<int>(type: "int", nullable: true),
                    ProveedorId = table.Column<int>(type: "int", nullable: true),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoOriginal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoPendiente = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InteresesMora = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NumeroFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasPorPagar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuentasPorPagar_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CuentasPorPagar_Tarjetas_TarjetaId",
                        column: x => x.TarjetaId,
                        principalTable: "Tarjetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    TarjetaId = table.Column<int>(type: "int", nullable: true),
                    ProveedorId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Origen = table.Column<int>(type: "int", nullable: false),
                    NotasIA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacciones_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transacciones_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transacciones_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transacciones_Tarjetas_TarjetaId",
                        column: x => x.TarjetaId,
                        principalTable: "Tarjetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AsientosContables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransaccionId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsientosContables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsientosContables_Transacciones_TransaccionId",
                        column: x => x.TransaccionId,
                        principalTable: "Transacciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PagosCuentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuentaPorCobrарId = table.Column<int>(type: "int", nullable: true),
                    CuentaPorPagarId = table.Column<int>(type: "int", nullable: true),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransaccionId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagosCuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagosCuentas_CuentasPorCobrar_CuentaPorCobrарId",
                        column: x => x.CuentaPorCobrарId,
                        principalTable: "CuentasPorCobrar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagosCuentas_CuentasPorPagar_CuentaPorPagarId",
                        column: x => x.CuentaPorPagarId,
                        principalTable: "CuentasPorPagar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagosCuentas_Transacciones_TransaccionId",
                        column: x => x.TransaccionId,
                        principalTable: "Transacciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recordatorios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRecordatorio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Enviado = table.Column<bool>(type: "bit", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransaccionId = table.Column<int>(type: "int", nullable: true),
                    CuentaPorPagarId = table.Column<int>(type: "int", nullable: true),
                    CuentaPorCobrarId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recordatorios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recordatorios_CuentasPorCobrar_CuentaPorCobrarId",
                        column: x => x.CuentaPorCobrarId,
                        principalTable: "CuentasPorCobrar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recordatorios_CuentasPorPagar_CuentaPorPagarId",
                        column: x => x.CuentaPorPagarId,
                        principalTable: "CuentasPorPagar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recordatorios_Transacciones_TransaccionId",
                        column: x => x.TransaccionId,
                        principalTable: "Transacciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesAsientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsientoContableId = table.Column<int>(type: "int", nullable: false),
                    CuentaContableId = table.Column<int>(type: "int", nullable: false),
                    Debe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Haber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesAsientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesAsientos_AsientosContables_AsientoContableId",
                        column: x => x.AsientoContableId,
                        principalTable: "AsientosContables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAsientos_CuentasContables_CuentaContableId",
                        column: x => x.CuentaContableId,
                        principalTable: "CuentasContables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "CategoriaPadreId", "Color", "CreatedAt", "Descripcion", "Icono", "IsActive", "Nombre", "Tipo", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, "#FF5733", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Servicios Básicos", 1, null },
                    { 2, null, "#33FF57", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Alimentación", 2, null },
                    { 3, null, "#3357FF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Transporte", 2, null },
                    { 4, null, "#FFD700", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Salario", 3, null },
                    { 5, null, "#FF33F5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Entretenimiento", 2, null }
                });

            migrationBuilder.InsertData(
                table: "CuentasContables",
                columns: new[] { "Id", "Codigo", "CreatedAt", "CuentaPadreId", "Descripcion", "EsDeMovimiento", "IsActive", "Naturaleza", "Nivel", "Nombre", "Tipo", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, true, 1, 1, "ACTIVO", 1, null },
                    { 5, "2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, true, 2, 1, "PASIVO", 2, null },
                    { 9, "3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, true, 2, 1, "PATRIMONIO", 3, null },
                    { 11, "4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, true, 2, 1, "INGRESOS", 4, null },
                    { 14, "5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, true, 1, 1, "GASTOS", 5, null },
                    { 2, "1.1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, false, true, 1, 2, "ACTIVO CORRIENTE", 1, null },
                    { 6, "2.1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, false, true, 2, 2, "PASIVO CORRIENTE", 2, null },
                    { 10, "3.1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, true, true, 2, 2, "Capital", 3, null },
                    { 12, "4.1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null, false, true, 2, 2, "Ingresos Operacionales", 4, null },
                    { 15, "5.1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null, false, true, 1, 2, "Gastos Fijos", 5, null },
                    { 17, "5.2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null, false, true, 1, 2, "Gastos Variables", 5, null },
                    { 3, "1.1.1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, true, true, 1, 3, "Efectivo y Equivalentes", 1, null },
                    { 4, "1.1.2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, true, true, 1, 3, "Cuentas por Cobrar", 1, null },
                    { 7, "2.1.1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, true, true, 2, 3, "Cuentas por Pagar", 2, null },
                    { 8, "2.1.2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, true, true, 2, 3, "Tarjetas de Crédito", 2, null },
                    { 13, "4.1.1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, true, true, 2, 3, "Salarios", 4, null },
                    { 16, "5.1.1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null, true, true, 1, 3, "Servicios Básicos", 5, null },
                    { 18, "5.2.1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, null, true, true, 1, 3, "Alimentación", 5, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsientosContables_Numero",
                table: "AsientosContables",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AsientosContables_TransaccionId",
                table: "AsientosContables",
                column: "TransaccionId",
                unique: true,
                filter: "[TransaccionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_CategoriaPadreId",
                table: "Categorias",
                column: "CategoriaPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasContables_Codigo",
                table: "CuentasContables",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CuentasContables_CuentaPadreId",
                table: "CuentasContables",
                column: "CuentaPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasPorCobrar_ClienteId",
                table: "CuentasPorCobrar",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasPorPagar_ProveedorId",
                table: "CuentasPorPagar",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasPorPagar_TarjetaId",
                table: "CuentasPorPagar",
                column: "TarjetaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsientos_AsientoContableId",
                table: "DetallesAsientos",
                column: "AsientoContableId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsientos_CuentaContableId",
                table: "DetallesAsientos",
                column: "CuentaContableId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosCuentas_CuentaPorCobrарId",
                table: "PagosCuentas",
                column: "CuentaPorCobrарId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosCuentas_CuentaPorPagarId",
                table: "PagosCuentas",
                column: "CuentaPorPagarId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosCuentas_TransaccionId",
                table: "PagosCuentas",
                column: "TransaccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Recordatorios_CuentaPorCobrarId",
                table: "Recordatorios",
                column: "CuentaPorCobrarId");

            migrationBuilder.CreateIndex(
                name: "IX_Recordatorios_CuentaPorPagarId",
                table: "Recordatorios",
                column: "CuentaPorPagarId");

            migrationBuilder.CreateIndex(
                name: "IX_Recordatorios_TransaccionId",
                table: "Recordatorios",
                column: "TransaccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CategoriaId",
                table: "Transacciones",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_ClienteId",
                table: "Transacciones",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_ProveedorId",
                table: "Transacciones",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_TarjetaId",
                table: "Transacciones",
                column: "TarjetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesAsientos");

            migrationBuilder.DropTable(
                name: "PagosCuentas");

            migrationBuilder.DropTable(
                name: "Recordatorios");

            migrationBuilder.DropTable(
                name: "AsientosContables");

            migrationBuilder.DropTable(
                name: "CuentasContables");

            migrationBuilder.DropTable(
                name: "CuentasPorCobrar");

            migrationBuilder.DropTable(
                name: "CuentasPorPagar");

            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Tarjetas");
        }
    }
}
