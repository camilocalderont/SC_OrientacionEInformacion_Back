using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dominio.Models.AtencionesWeb;

namespace Persistencia.FluentConfig.AtencionWebConfig
{
    public class AtencionWebConfig
    {
        public AtencionWebConfig(EntityTypeBuilder<AtencionWeb> entity)
        {

            entity.ToTable("AtencionWeb");
            entity.HasKey(p => p.Id);


            entity
           .HasMany(p => p.AtencionReasignaciones)
           .WithOne(p => p.AtencionesWeb)
           .HasForeignKey(p => p.AtencionWebId)              // Código de la caso asociado
           .HasConstraintName("FK_CasoW_A_CasoW")
           .OnDelete(DeleteBehavior.Restrict);


            entity
            .HasMany(p => p.AtencionSeguimientos)
            .WithOne(p => p.AtencionesWeb)
            .HasForeignKey(p => p.AtencionWebId)              // Código de la caso asociado
            .HasConstraintName("FK_CasoW_A_CasoWS")
            .OnDelete(DeleteBehavior.Restrict);

            entity
           .HasMany(p => p.AtencionAnexos)
           .WithOne(p => p.AtencionesWeb)
           .HasForeignKey(p => p.AtencionWebId)              // Código de la caso asociado
           .HasConstraintName("FK_CasoW_A_CasoWAn")
           .OnDelete(DeleteBehavior.Restrict);

           // entity.HasOne(p => p.PersonasWeb)                // tiene uno
           //.WithMany(p => p.AtencionesWeb);                  // con muchos




            entity.Property(p => p.PersonaWebId).IsRequired();
            entity.Property(p => p.DtFechaRegistro).IsRequired();
            entity.Property(p => p.UsuarioId).IsRequired();
            entity.Property(p => p.CanalAtencionId).IsRequired();
            entity.Property(p => p.TipoSolicitudId).IsRequired();
            entity.Property(p => p.MotivoId).IsRequired();
            entity.Property(p => p.SubMotivoId).IsRequired();
            entity.Property(p => p.TxAclaracionMotivo).IsRequired();
            entity.Property(p => p.TxAsuntoCorreo).IsRequired();
            entity.Property(p => p.BProcesoFallido).IsRequired(false);
            entity.Property(p => p.TipoSolicitudId).IsRequired();
            entity.Property(p => p.TipoGestionId).IsRequired(false);
            entity.Property(p => p.EstadoId).IsRequired();
        }
    }
}
