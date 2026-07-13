using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestorTareasIntegrador.Migrations
{
    /// <inheritdoc />
    public partial class InicialConSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Prioridad = table.Column<string>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Completada = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreadaEn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Estudio" },
                    { 2, "Trabajo" },
                    { 3, "Personal" },
                    { 4, "Hogar" },
                    { 5, "Proyectos" }
                });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "Id", "CategoriaId", "Completada", "CreadaEn", "Descripcion", "FechaVencimiento", "Prioridad", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, true, new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Verificar con dotnet --version", new DateTime(2026, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alta", "Instalar .NET 10 SDK" },
                    { 2, 1, false, new DateTime(2026, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clase 3 - binding bidireccional", new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Media", "Repasar Data Binding" },
                    { 3, 5, false, new DateTime(2026, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Con DataAnnotationsValidator", new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alta", "Armar EditForm de alta" },
                    { 4, 5, false, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Archivos .razor.css", new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baja", "Configurar CSS Isolation" },
                    { 5, 5, false, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cards responsive", new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Media", "Aplicar grilla Bootstrap" },
                    { 6, 5, false, new DateTime(2026, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "JS Interop + localStorage", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Media", "Implementar modo oscuro" },
                    { 7, 2, true, new DateTime(2026, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "EF Core + SQLite", new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alta", "Crear TareasDbContext" },
                    { 8, 2, false, new DateTime(2026, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "dotnet ef migrations add", new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alta", "Migraciones y seed" },
                    { 9, 3, false, new DateTime(2026, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Skip/Take con conteo total", new DateTime(2026, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Media", "Agregar paginación" },
                    { 10, 4, false, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revisar checklist de entrega", new DateTime(2026, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baja", "Preparar demo final" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_CategoriaId",
                table: "Tareas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_Prioridad",
                table: "Tareas",
                column: "Prioridad");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_Titulo",
                table: "Tareas",
                column: "Titulo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
