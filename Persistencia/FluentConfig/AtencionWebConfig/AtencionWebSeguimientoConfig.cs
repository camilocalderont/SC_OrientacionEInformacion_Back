using Dominio.Models.AtencionesWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionWebConfig
{
    public class AtencionWebSeguimientoConfig
    {
        public AtencionWebSeguimientoConfig(EntityTypeBuilder<AtencionWebSeguimiento> entity)
        {

            entity.ToTable("AtencionWebSeguimiento");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.AtencionWebId).IsRequired();
            entity.Property(p => p.VcDescripcion).IsRequired(false).HasMaxLength(500);
            entity.Property(p => p.BCierraCaso).IsRequired();
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
        }
    }
}
