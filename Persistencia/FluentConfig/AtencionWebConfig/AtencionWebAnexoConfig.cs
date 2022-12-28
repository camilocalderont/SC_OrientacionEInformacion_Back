using Dominio.Models.AtencionesWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionWebConfig
{
    public class AtencionWebAnexoConfig
    {
        public AtencionWebAnexoConfig(EntityTypeBuilder<AtencionWebAnexo> entity)
        {
            entity.ToTable("AtencionWebAnexo");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.AtencionWebId).IsRequired();
            entity.Property(p => p.VcNombre).IsRequired().HasMaxLength(150);
            entity.Property(p => p.VcDescripcion).IsRequired(false).HasMaxLength(500);
            entity.Property(p => p.IBytes).IsRequired();
            entity.Property(p => p.VcRuta).IsRequired(true).HasMaxLength(500);
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
        }

    }
}
