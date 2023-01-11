using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionIndividualConfig
{
    public class PersonaConfig
    {
        public PersonaConfig(EntityTypeBuilder<Persona>entity)
        {
            entity.ToTable("Persona");
            entity.HasKey(x => x.Id);

            entity
           .HasMany(p => p.AtencionIndividual)
           .WithOne(p => p.Personas)
           .HasForeignKey(p => p.PersonaId)              // Código de la caso asociado
           .HasConstraintName("FK_Persona_A_CasoI")
           .OnDelete(DeleteBehavior.Restrict);

            entity
           .HasMany(p => p.PersonaAfiliaciones)
           .WithOne(p => p.Personas)
           .HasForeignKey(p => p.PersonaId)              // Código de la caso asociado
           .HasConstraintName("FK_Persona_A_PAfiliacion")
           .OnDelete(DeleteBehavior.Restrict);

            entity
           .HasMany(p => p.PersonaContactos)
           .WithOne(p => p.Personas)
           .HasForeignKey(p => p.PersonaId)              // Código de la caso asociado
           .HasConstraintName("FK_Usuario_ A_PContacto")
           .OnDelete(DeleteBehavior.Restrict);


            entity.Property(p => p.TipoDocumentoId).IsRequired();
            entity.Property(p => p.VcDocumento).IsRequired().HasMaxLength(50);
            entity.Property(p => p.VcPrimerNombre).IsRequired().HasMaxLength(100);
            entity.Property(p => p.VcSegundoNombre).IsRequired(false).HasMaxLength(100);
            entity.Property(p => p.VcPrimerApellido).IsRequired().HasMaxLength(100);
            entity.Property(p => p.VcSegundoApellido).IsRequired(false).HasMaxLength(100);
            entity.Property(p => p.GeneroId).IsRequired();
            entity.Property(p => p.VcOtroGenero).IsRequired(false).HasMaxLength(100);
            entity.Property(p => p.VcNombreIdentitario).IsRequired(false).HasMaxLength(100);
            entity.Property(p => p.OrientacionSexualId).IsRequired();
            entity.Property(p => p.VcOtraOrientacionSexual).IsRequired(false).HasMaxLength(100);
            entity.Property(p => p.SexoId).IsRequired();
            entity.Property(p => p.DtFechaNacimiento).IsRequired(false);
            entity.Property(p => p.EnfoquePoblacionalId).IsRequired();
            entity.Property(p => p.HechoVictimizanteId).IsRequired(false);
            entity.Property(p => p.DepartamentoOrigenVictimaId).IsRequired(false);
            entity.Property(p => p.MunicipioOrigenVictimaId).IsRequired(false);
            entity.Property(p => p.EtniaId).IsRequired();
            entity.Property(p => p.SubEtniaId).IsRequired(false);
            entity.Property(p => p.PoblacionPrioritariaId).IsRequired();
            entity.Property(p => p.SubPoblacionPrioritariaId).IsRequired(false);
            entity.Property(p => p.VcCorreo).IsRequired(false).HasMaxLength(100);
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
            entity.Property(p => p.DtFechaActualizacion).IsRequired(false);
            entity.Property(p => p.UsuarioActualizacionId).IsRequired(false);
        }
    } 
}
