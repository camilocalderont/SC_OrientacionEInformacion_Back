using Dominio.Models.AtencionesGrupales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionGrupalConfig
{
    public class AtencionGrupalConfig
    {
        public AtencionGrupalConfig(EntityTypeBuilder<AtencionGrupal> entity)
        {
            entity.ToTable("AtencionGrupal");
            entity.HasKey(p => p.Id);

            entity
          .HasMany(p => p.AtencionGrupalesAnexos)
          .WithOne(p => p.AtencionGrupales)
          .HasForeignKey(p => p.AtencionGrupalId)              // Código de la caso asociado
          .HasConstraintName("PK_atencionGrupalId")
          .OnDelete(DeleteBehavior.Restrict);


            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
            entity.Property(p => p.INumeroUsuarios).IsRequired();
            entity.Property(p => p.TipoSolicitudId).IsRequired();
            entity.Property(p => p.MotivoId).IsRequired();
            entity.Property(p => p.SubMotivoId).IsRequired();
            entity.Property(p => p.TxAclaracionMotivo).IsRequired();
            entity.Property(p => p.TiempoDuracionId).IsRequired();
            entity.Property(p => p.TipoActividadId).IsRequired();
            entity.Property(p => p.LocalidadId).IsRequired();
            entity.Property(p => p.VcLugar).IsRequired().HasMaxLength(200);
        }
    }
}
