using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionIndividualConfig
{
    public class PersonaAfiliacionConfig
    {
        public PersonaAfiliacionConfig(EntityTypeBuilder<PersonaAfiliacion> entity)
        {
            entity.ToTable("PersonaAfiliacion");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.PersonaId).IsRequired();
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
            entity.Property(p => p.RegimenId).IsRequired();
            entity.Property(p => p.AseguradoraId).IsRequired();
            entity.Property(p => p.EstadoAfiliacionId).IsRequired();
            entity.Property(p => p.NivelSisbenId).IsRequired();
            entity.Property(p => p.InstitucionInstrumentoVinculadoId).IsRequired(false);
        }
    }
}
