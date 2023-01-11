using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionIndividualConfig
{
    public class PersonaContactoConfig
    {
        public PersonaContactoConfig(EntityTypeBuilder<PersonaContacto> entity)
        {
            entity.ToTable("PersonaContacto");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.PersonaId).IsRequired();
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
            entity.Property(p => p.PaisId).IsRequired();
            entity.Property(p => p.DepartamentoId).IsRequired();
            entity.Property(p => p.MunicipioId).IsRequired();
            entity.Property(p => p.LocalidadId).IsRequired();
            entity.Property(p => p.UpzId).IsRequired(false);
            entity.Property(p => p.BarrioId).IsRequired(false);
            entity.Property(p => p.ZonaId).IsRequired();
            entity.Property(p => p.VcDireccion).IsRequired().HasMaxLength(100);
            entity.Property(p => p.TxDatosContactoAclaraciones).IsRequired(false);
            entity.Property(p => p.VcTelefono1).IsRequired(false);
            entity.Property(p => p.VcTelefono2).IsRequired(false);
        }
    }

}
