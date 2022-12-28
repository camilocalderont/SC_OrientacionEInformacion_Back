using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionIndividualConfig
{
    public class AtencionIndividualActorConfig
    {
        public AtencionIndividualActorConfig(EntityTypeBuilder<AtencionIndividualActor> entity)
        {
            entity.ToTable("AtencionIndividualActor");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.AtencionIndividualId).IsRequired();
            entity.Property(p => p.TipoActorId).IsRequired();
            entity.Property(p => p.TipoId).IsRequired();
            entity.Property(p => p.SedeId).IsRequired();
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
        }
    }
}
