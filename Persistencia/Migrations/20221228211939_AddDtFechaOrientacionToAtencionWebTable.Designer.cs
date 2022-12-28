﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistencia.Context;

#nullable disable

namespace Persistencia.Migrations
{
    [DbContext(typeof(OrientacionDbContext))]
    [Migration("20221228211939_AddDtFechaOrientacionToAtencionWebTable")]
    partial class AddDtFechaOrientacionToAtencionWebTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Dominio.Models.AtencionesGrupales.AtencionGrupal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DtFechaOrientacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("INumeroUsuarios")
                        .HasColumnType("int");

                    b.Property<long>("LocalidadId")
                        .HasColumnType("bigint");

                    b.Property<long>("MotivoId")
                        .HasColumnType("bigint");

                    b.Property<long>("SubMotivoId")
                        .HasColumnType("bigint");

                    b.Property<long>("TiempoDuracionId")
                        .HasColumnType("bigint");

                    b.Property<long>("TipoActividadId")
                        .HasColumnType("bigint");

                    b.Property<long>("TipoSolicitudId")
                        .HasColumnType("bigint");

                    b.Property<string>("TxAclaracionMotivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcLugar")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("AtencionGrupal", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesGrupales.AtencionGrupalAnexo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AtencionGrupalId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IBytes")
                        .HasColumnType("int");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcDescripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("VcNombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("VcRuta")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("AtencionGrupalId");

                    b.ToTable("AtencionGrupalAnexo", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividual", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("CanalAtencionId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<long>("EstadoId")
                        .HasColumnType("bigint");

                    b.Property<long>("MotivoId")
                        .HasColumnType("bigint");

                    b.Property<long>("PersonaId")
                        .HasColumnType("bigint");

                    b.Property<long>("SubMotivoId")
                        .HasColumnType("bigint");

                    b.Property<long>("TipoSolicitudId")
                        .HasColumnType("bigint");

                    b.Property<string>("TxAclaracionMotivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TxGestionRealizada")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcRadicadoBte")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VcTurnoSat")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.ToTable("AtencionIndividual", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividualActor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AtencionIndividualId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<long>("SedeId")
                        .HasColumnType("bigint");

                    b.Property<long>("TipoActorId")
                        .HasColumnType("bigint");

                    b.Property<long>("TipoId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AtencionIndividualId");

                    b.ToTable("AtencionIndividualActor", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividualAnexo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AtencionIndividualId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<long>("IBytes")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcDescripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("VcNombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("VcRuta")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("AtencionIndividualId");

                    b.ToTable("AtencionIndividualAnexo", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividualReasignacion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AtencionIndividualId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DtFechaAsignacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtFechaReAsignacion")
                        .HasColumnType("datetime2");

                    b.Property<long>("UsuarioActualId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioAsignaId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcDescripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("AtencionIndividualId");

                    b.ToTable("AtencionIndividualReasignacion", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividualSeguimiento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AtencionIndividualId")
                        .HasColumnType("bigint");

                    b.Property<bool>("BCierraCaso")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcDescripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("AtencionIndividualId");

                    b.ToTable("AtencionSeguimiento", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.Persona", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("DepartamentoOrigenVictimaId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DtFechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtFechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<long>("EnfoquePoblacionalId")
                        .HasColumnType("bigint");

                    b.Property<long>("EtniaId")
                        .HasColumnType("bigint");

                    b.Property<long>("GeneroId")
                        .HasColumnType("bigint");

                    b.Property<long?>("HechoVictimizanteId")
                        .HasColumnType("bigint");

                    b.Property<long?>("MunicipioOrigenVictimaId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrientacionSexualId")
                        .HasColumnType("bigint");

                    b.Property<long>("PoblacionPrioritariaId")
                        .HasColumnType("bigint");

                    b.Property<long>("SexoId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SubEtniaId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SubPoblacionPrioritariaId")
                        .HasColumnType("bigint");

                    b.Property<long>("TipoDocumentoId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioActualizacionId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcCorreo")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcDocumento")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VcNombreIdentitario")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcOtraOrientacionSexual")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcOtroGenero")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcPrimerApellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcPrimerNombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcSegundoApellido")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcSegundoNombre")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Persona", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.PersonaAfiliacion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AseguradoraId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<long>("EstadoAfiliacionId")
                        .HasColumnType("bigint");

                    b.Property<long?>("InstitucionInstrumentoVinculadoId")
                        .HasColumnType("bigint");

                    b.Property<long>("NivelSisbenId")
                        .HasColumnType("bigint");

                    b.Property<long>("PersonaId")
                        .HasColumnType("bigint");

                    b.Property<long>("RegimenId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.ToTable("PersonaAfiliacion", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.PersonaContacto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("BarrioId")
                        .HasColumnType("bigint");

                    b.Property<long>("DepartamentoId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<long>("LocalidadId")
                        .HasColumnType("bigint");

                    b.Property<long>("PaisId")
                        .HasColumnType("bigint");

                    b.Property<long>("PersonaId")
                        .HasColumnType("bigint");

                    b.Property<string>("TxDatosContactoAclaraciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UpzId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcDireccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VcTelefono1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VcTelefono2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ZonaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.ToTable("PersonaContacto", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.AtencionWeb", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool?>("BProcesoFallido")
                        .HasColumnType("bit");

                    b.Property<long>("CanalAtencionId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DtFechaOrientacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<long>("EstadoId")
                        .HasColumnType("bigint");

                    b.Property<long>("MotivoId")
                        .HasColumnType("bigint");

                    b.Property<long>("PersonaWebId")
                        .HasColumnType("bigint");

                    b.Property<long>("SubMotivoId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TipoGestionId")
                        .HasColumnType("bigint");

                    b.Property<long>("TipoProcesoFallidoId")
                        .HasColumnType("bigint");

                    b.Property<long>("TipoSolicitudId")
                        .HasColumnType("bigint");

                    b.Property<string>("TxAclaracionMotivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TxAsuntoCorreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PersonaWebId");

                    b.ToTable("AtencionWeb", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.AtencionWebAnexo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AtencionWebId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IBytes")
                        .HasColumnType("int");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcDescripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("VcNombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("VcRuta")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("AtencionWebId");

                    b.ToTable("AtencionWebAnexo", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.AtencionWebReasignacion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AtencionWebId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DtFechaAsignacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtFechaReAsignacion")
                        .HasColumnType("datetime2");

                    b.Property<long>("UsuarioActualId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioAsignaId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcDescripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("AtencionWebId");

                    b.ToTable("AtencionWebReasignacion", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.AtencionWebSeguimiento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AtencionWebId")
                        .HasColumnType("bigint");

                    b.Property<bool>("BCierraCaso")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcDescripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AtencionWebId");

                    b.ToTable("AtencionWebSeguimiento");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.PersonaWeb", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DtFechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtFechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<long>("UsuarioActualizacionId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("VcCorreo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcPrimerApellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcPrimerNombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcSegundoApellido")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcSegundoNombre")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VcTelefono1")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VcTelefono2")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("PersonaWeb", (string)null);
                });

            modelBuilder.Entity("Dominio.Models.AtencionesGrupales.AtencionGrupalAnexo", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesGrupales.AtencionGrupal", "AtencionGrupales")
                        .WithMany("AtencionGrupalesAnexos")
                        .HasForeignKey("AtencionGrupalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("PK_atencionGrupalId");

                    b.Navigation("AtencionGrupales");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividual", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesIndividuales.Persona", "Personas")
                        .WithMany("AtencionIndividual")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Persona_A_CasoI");

                    b.Navigation("Personas");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividualActor", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesIndividuales.AtencionIndividual", "AtencionIndividual")
                        .WithMany("AtencionActores")
                        .HasForeignKey("AtencionIndividualId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_CasoI_A_CasoIAc");

                    b.Navigation("AtencionIndividual");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividualAnexo", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesIndividuales.AtencionIndividual", "AtencionIndividual")
                        .WithMany("AtencionAnexos")
                        .HasForeignKey("AtencionIndividualId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_CasoI_A_CasoIAn");

                    b.Navigation("AtencionIndividual");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividualReasignacion", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesIndividuales.AtencionIndividual", "AtencionIndividual")
                        .WithMany("AtencionReasignaciones")
                        .HasForeignKey("AtencionIndividualId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_CasoI_A_CasoIR");

                    b.Navigation("AtencionIndividual");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividualSeguimiento", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesIndividuales.AtencionIndividual", "AtencionIndividual")
                        .WithMany("AtencionSeguimientos")
                        .HasForeignKey("AtencionIndividualId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_CasoI_A_CasoIS");

                    b.Navigation("AtencionIndividual");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.PersonaAfiliacion", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesIndividuales.Persona", "Personas")
                        .WithMany("PersonaAfiliaciones")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Persona_A_PAfiliacion");

                    b.Navigation("Personas");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.PersonaContacto", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesIndividuales.Persona", "Personas")
                        .WithMany("PersonaContactos")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Usuario_ A_PContacto");

                    b.Navigation("Personas");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.AtencionWeb", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesWeb.PersonaWeb", "PersonasWeb")
                        .WithMany("AtencionesWeb")
                        .HasForeignKey("PersonaWebId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_PersonaWebId");

                    b.Navigation("PersonasWeb");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.AtencionWebAnexo", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesWeb.AtencionWeb", "AtencionesWeb")
                        .WithMany("AtencionAnexos")
                        .HasForeignKey("AtencionWebId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_CasoW_A_CasoWAn");

                    b.Navigation("AtencionesWeb");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.AtencionWebReasignacion", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesWeb.AtencionWeb", "AtencionesWeb")
                        .WithMany("AtencionReasignaciones")
                        .HasForeignKey("AtencionWebId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_CasoW_A_CasoW");

                    b.Navigation("AtencionesWeb");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.AtencionWebSeguimiento", b =>
                {
                    b.HasOne("Dominio.Models.AtencionesWeb.AtencionWeb", "AtencionesWeb")
                        .WithMany("AtencionSeguimientos")
                        .HasForeignKey("AtencionWebId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_CasoW_A_CasoWS");

                    b.Navigation("AtencionesWeb");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesGrupales.AtencionGrupal", b =>
                {
                    b.Navigation("AtencionGrupalesAnexos");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.AtencionIndividual", b =>
                {
                    b.Navigation("AtencionActores");

                    b.Navigation("AtencionAnexos");

                    b.Navigation("AtencionReasignaciones");

                    b.Navigation("AtencionSeguimientos");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesIndividuales.Persona", b =>
                {
                    b.Navigation("AtencionIndividual");

                    b.Navigation("PersonaAfiliaciones");

                    b.Navigation("PersonaContactos");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.AtencionWeb", b =>
                {
                    b.Navigation("AtencionAnexos");

                    b.Navigation("AtencionReasignaciones");

                    b.Navigation("AtencionSeguimientos");
                });

            modelBuilder.Entity("Dominio.Models.AtencionesWeb.PersonaWeb", b =>
                {
                    b.Navigation("AtencionesWeb");
                });
#pragma warning restore 612, 618
        }
    }
}
