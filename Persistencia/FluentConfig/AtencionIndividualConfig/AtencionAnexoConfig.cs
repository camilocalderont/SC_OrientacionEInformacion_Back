using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionIndividualConfig
{
    public class AtencionAnexoConfig
    {
        public AtencionAnexoConfig(EntityTypeBuilder<AtencionAnexo> entity)
        {
            entity.ToTable("AtencionAnexo");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.AtencionIndividualId).IsRequired();
            entity.Property(p => p.VcNombre).IsRequired().HasMaxLength(150);
            entity.Property(p => p.VcDescripcion).IsRequired(false).HasMaxLength(500); ;
            entity.Property(p => p.IBytes).IsRequired();
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
        }
    }
}
