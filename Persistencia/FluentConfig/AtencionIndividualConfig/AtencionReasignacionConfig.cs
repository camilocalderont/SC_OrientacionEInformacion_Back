using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistencia.FluentConfig.AtencionIndividualConfig
{
    public class AtencionReasignacionConfig
    {
        public AtencionReasignacionConfig(EntityTypeBuilder<AtencionReasignacion> entity)
        {
            entity.ToTable("AtencionReasignacion");
            entity.HasKey(x => x.Id);

            entity.Property(p => p.AtencionIndividualId).IsRequired();
            entity.Property(p => p.VcDescripcion).IsRequired(false).HasMaxLength(500);
            entity.Property(p => p.DtFechaAsignacion).IsRequired();
            entity.Property(p => p.UsuarioAsignaId).IsRequired();
            entity.Property(p => p.DtFechaReAsignacion).IsRequired();
            entity.Property(p => p.UsuarioActualId).IsRequired();
        }

    }
}
