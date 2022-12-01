using Dominio.Models.AtencionesWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionWebConfig
{
    public class AtencionWebReasignacionConfig
    {
        public AtencionWebReasignacionConfig(EntityTypeBuilder<AtencionWebReasignacion> entity)
        {
            entity.ToTable("AtencionWebReasignacion");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.AtencionWebId).IsRequired();
            entity.Property(p => p.VcDescripcion).IsRequired(false).HasMaxLength(500);
            entity.Property(p => p.DtFechaAsignacion).IsRequired();
            entity.Property(p => p.UsuarioAsignaId).IsRequired();
            entity.Property(p => p.DtFechaReAsignacion).IsRequired();
            entity.Property(p => p.UsuarioActualId).IsRequired();
        }
    }
}

