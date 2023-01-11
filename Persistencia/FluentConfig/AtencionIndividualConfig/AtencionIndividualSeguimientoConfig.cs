using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionIndividualConfig
{
    public class AtencionIndividualSeguimientoConfig
    {
        public AtencionIndividualSeguimientoConfig(EntityTypeBuilder<AtencionIndividualSeguimiento>entity)
        {
            entity.ToTable("AtencionIndividualSeguimiento");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.AtencionIndividualId).IsRequired();
            entity.Property(p => p.VcDescripcion).IsRequired().HasMaxLength(500);
            entity.Property(p => p.BCierraCaso).IsRequired();
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
        }

    }
}
