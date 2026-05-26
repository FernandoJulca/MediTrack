using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediTrack.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class CreacionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Icono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UrlFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContrasenaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Dni = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    UrlFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    EspecialidadId = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    SedeId = table.Column<int>(type: "int", nullable: false),
                    EspecialidadId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citas_Especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Sedes_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Usuarios_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Usuarios_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HorariosDoctores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time", nullable: false),
                    DuracionCitaMinutos = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    SedeId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorariosDoctores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorariosDoctores_Sedes_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HorariosDoctores_Usuarios_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InformesMedicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sintomas = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Diagnostico = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Tratamiento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Receta = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    FechaInforme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CitaId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformesMedicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformesMedicos_Citas_CitaId",
                        column: x => x.CitaId,
                        principalTable: "Citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_DoctorId",
                table: "Citas",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_EspecialidadId",
                table: "Citas",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PacienteId",
                table: "Citas",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_SedeId",
                table: "Citas",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosDoctores_DoctorId",
                table: "HorariosDoctores",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosDoctores_SedeId",
                table: "HorariosDoctores",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_InformesMedicos_CitaId",
                table: "InformesMedicos",
                column: "CitaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Dni",
                table: "Usuarios",
                column: "Dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EspecialidadId",
                table: "Usuarios",
                column: "EspecialidadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorariosDoctores");

            migrationBuilder.DropTable(
                name: "InformesMedicos");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Especialidades");
        }
    }
}
