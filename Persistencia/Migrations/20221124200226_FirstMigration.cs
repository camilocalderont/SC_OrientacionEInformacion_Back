using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtencionGrupal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    INumeroUsuarios = table.Column<int>(type: "int", nullable: false),
                    TipoSolicitudId = table.Column<long>(type: "bigint", nullable: false),
                    MotivoId = table.Column<long>(type: "bigint", nullable: false),
                    SubMotivoId = table.Column<long>(type: "bigint", nullable: false),
                    TxAclaracionMotivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TiempoDuracionId = table.Column<long>(type: "bigint", nullable: false),
                    TipoActividadId = table.Column<long>(type: "bigint", nullable: false),
                    LocalidadId = table.Column<long>(type: "bigint", nullable: false),
                    VcLugar = table.Column<long>(type: "bigint", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionGrupal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VcDocumento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VcPrimerNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VcSegundoNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VcPrimerApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VcSegundoApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VcOtroGenero = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VcNombreIdentitario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrientacionSexualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VcOtraOrientacionSexual = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SexoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtFechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnfoquePoblacionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HechoVictimizanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartamentoOrigenVictimaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MunicipioOrigenVictimaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EtniaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubEtniaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PoblacionPrioritariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubPoblacionPrioritariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VcCorreo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtFechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioActualizacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonaWeb",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VcPrimerNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VcSegundoNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VcPrimerApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VcSegundoApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VcCorreo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VcTelefono1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VcTelefono2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtFechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioActualizacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaWeb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AtencionGrupalAnexo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtencionGrupalId = table.Column<long>(type: "bigint", nullable: false),
                    VcNombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VcDescripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IBytes = table.Column<int>(type: "int", nullable: false),
                    VcRuta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VcUrl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionGrupalAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "PK_atencionGrupalId",
                        column: x => x.AtencionGrupalId,
                        principalTable: "AtencionGrupal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtencionIndividual",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<long>(type: "bigint", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanalAtencionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VcTurnoSat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TipoSolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MotivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubMotivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TxAclaracionMotivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TxGestionRealizada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VcRadicadoBte = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionIndividual", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_A_CasoI",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonaAfiliacion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<long>(type: "bigint", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegimenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AseguradoraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstadoAfiliacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NivelSisbenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstitucionInstrumentoVinculadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaAfiliacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_A_PAfiliacion",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonaContacto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<long>(type: "bigint", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocalidadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpzId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarrioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZonaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VcDireccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TxDatosContactoAclaraciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VcTelefono1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VcTelefono2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaContacto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_ A_PContacto",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtencionWeb",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaWebId = table.Column<long>(type: "bigint", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanalAtencionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoSolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MotivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubMotivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TxAclaracionMotivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TxAsuntoCorreo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BProcesoFallido = table.Column<bool>(type: "bit", nullable: false),
                    TipoProcesoFallidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoGestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionWeb", x => x.Id);
                    table.ForeignKey(
                        name: "   FK_CasoW_A_CasoW",
                        column: x => x.PersonaWebId,
                        principalTable: "PersonaWeb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtencionActor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtencionIndividualId = table.Column<long>(type: "bigint", nullable: false),
                    TipoActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SedeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionActor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasoI_A_CasoIAc",
                        column: x => x.AtencionIndividualId,
                        principalTable: "AtencionIndividual",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtencionAnexo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtencionIndividualId = table.Column<long>(type: "bigint", nullable: false),
                    VcNombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VcDescripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IBytes = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasoI_A_CasoIAn",
                        column: x => x.AtencionIndividualId,
                        principalTable: "AtencionIndividual",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtencionReasignacion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtencionIndividualId = table.Column<long>(type: "bigint", nullable: false),
                    VcDescripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DtFechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioAsignaId = table.Column<long>(type: "bigint", nullable: false),
                    DtFechaReAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioActualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionReasignacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasoI_A_CasoIR",
                        column: x => x.AtencionIndividualId,
                        principalTable: "AtencionIndividual",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtencionSeguimiento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtencionIndividualId = table.Column<long>(type: "bigint", nullable: false),
                    VcDescripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BCierraCaso = table.Column<bool>(type: "bit", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionSeguimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasoI_A_CasoIS",
                        column: x => x.AtencionIndividualId,
                        principalTable: "AtencionIndividual",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtencionWebAnexo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtencionWebId = table.Column<long>(type: "bigint", nullable: false),
                    VcNombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VcDescripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IBytes = table.Column<int>(type: "int", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionWebAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasoW_A_CasoWAn",
                        column: x => x.AtencionWebId,
                        principalTable: "AtencionWeb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtencionWebReasignacion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtencionWebId = table.Column<long>(type: "bigint", nullable: false),
                    VcDescripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DtFechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioAsignaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtFechaReAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioActualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionWebReasignacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasoW_A_CasoW",
                        column: x => x.AtencionWebId,
                        principalTable: "AtencionWeb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtencionWebSeguimiento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtencionWebId = table.Column<long>(type: "bigint", nullable: false),
                    VcDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BCierraCaso = table.Column<bool>(type: "bit", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionWebSeguimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasoW_A_CasoWS",
                        column: x => x.AtencionWebId,
                        principalTable: "AtencionWeb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtencionActor_AtencionIndividualId",
                table: "AtencionActor",
                column: "AtencionIndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionAnexo_AtencionIndividualId",
                table: "AtencionAnexo",
                column: "AtencionIndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionGrupalAnexo_AtencionGrupalId",
                table: "AtencionGrupalAnexo",
                column: "AtencionGrupalId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionIndividual_PersonaId",
                table: "AtencionIndividual",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionReasignacion_AtencionIndividualId",
                table: "AtencionReasignacion",
                column: "AtencionIndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionSeguimiento_AtencionIndividualId",
                table: "AtencionSeguimiento",
                column: "AtencionIndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionWeb_PersonaWebId",
                table: "AtencionWeb",
                column: "PersonaWebId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionWebAnexo_AtencionWebId",
                table: "AtencionWebAnexo",
                column: "AtencionWebId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionWebReasignacion_AtencionWebId",
                table: "AtencionWebReasignacion",
                column: "AtencionWebId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionWebSeguimiento_AtencionWebId",
                table: "AtencionWebSeguimiento",
                column: "AtencionWebId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaAfiliacion_PersonaId",
                table: "PersonaAfiliacion",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaContacto_PersonaId",
                table: "PersonaContacto",
                column: "PersonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtencionActor");

            migrationBuilder.DropTable(
                name: "AtencionAnexo");

            migrationBuilder.DropTable(
                name: "AtencionGrupalAnexo");

            migrationBuilder.DropTable(
                name: "AtencionReasignacion");

            migrationBuilder.DropTable(
                name: "AtencionSeguimiento");

            migrationBuilder.DropTable(
                name: "AtencionWebAnexo");

            migrationBuilder.DropTable(
                name: "AtencionWebReasignacion");

            migrationBuilder.DropTable(
                name: "AtencionWebSeguimiento");

            migrationBuilder.DropTable(
                name: "PersonaAfiliacion");

            migrationBuilder.DropTable(
                name: "PersonaContacto");

            migrationBuilder.DropTable(
                name: "AtencionGrupal");

            migrationBuilder.DropTable(
                name: "AtencionIndividual");

            migrationBuilder.DropTable(
                name: "AtencionWeb");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "PersonaWeb");
        }
    }
}
