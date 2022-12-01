using Dominio.Models.AtencionesGrupales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionGrupalConfig
{
    public class AtencionGrupalAnexoConfig
    {
        public AtencionGrupalAnexoConfig(EntityTypeBuilder<AtencionGrupalAnexo> entity)
        {
            entity.ToTable("AtencionGrupalAnexo");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.AtencionGrupalId).IsRequired();
            entity.Property(p => p.VcNombre).IsRequired().HasMaxLength(150);
            entity.Property(p => p.VcDescripcion).IsRequired(false).HasMaxLength(500);
            entity.Property(p => p.IBytes).IsRequired();
            entity.Property(p => p.VcRuta).IsRequired(false).HasMaxLength(500);
            entity.Property(p => p.VcUrl).IsRequired(false).HasMaxLength(50);
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
        }
    }
}
