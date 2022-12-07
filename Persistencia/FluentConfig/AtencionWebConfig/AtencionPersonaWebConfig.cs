using Dominio.Models.AtencionesWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionWebConfig
{
    public class AtencionPersonaWebConfig
    {
        public AtencionPersonaWebConfig(EntityTypeBuilder<PersonaWeb> entity)
        {
            entity.ToTable("PersonaWeb");
            entity.HasKey(p => p.Id);


            entity
           .HasMany(p => p.AtencionesWeb)
           .WithOne(p => p.PersonasWeb)
           .HasForeignKey(p => p.PersonaWebId)              // Código de la caso asociado
           .HasConstraintName("FK_PersonaWebId")
           .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.VcPrimerNombre).IsRequired().HasMaxLength(100);
            entity.Property(p => p.VcSegundoNombre).IsRequired(false).HasMaxLength(100);
            entity.Property(p => p.VcPrimerApellido).IsRequired().HasMaxLength(100);
            entity.Property(p => p.VcSegundoApellido).IsRequired(false).HasMaxLength(100);
            entity.Property(p => p.VcCorreo).IsRequired().HasMaxLength(100);
            entity.Property(p => p.VcTelefono1).IsRequired(false).HasMaxLength(50);
            entity.Property(p => p.VcTelefono2).IsRequired(false).HasMaxLength(50);
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
            entity.Property(p => p.DtFechaActualizacion).IsRequired();
            entity.Property(p => p.UsuarioActualizacionId).IsRequired();
        }

    }
}
